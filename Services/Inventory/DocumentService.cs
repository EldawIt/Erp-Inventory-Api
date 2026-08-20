using ErpSystem.Helpers;

namespace ErpSystem.Services.Inventory;

public class DocumentService(ApplicationDbContext context, IUnitOfWork unitOfWork) : IDocumentService
{
    private readonly ApplicationDbContext _context =context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    

    public async Task<Result<DocumentResponse>> CreateDocument(DocumentRequest request, CancellationToken cancellationToken)
    {
        var validationResult = DocumentHelper.ValidateDocument(request);

        if (validationResult.IsFailure)
            return Result.Failure<DocumentResponse>(validationResult.Error!);


        var productIds = request.Details.Select(d => d.ProductItemId).Distinct().ToList();

        var existingProductsCount = await _context.ProductItems
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .CountAsync(cancellationToken);

        if (existingProductsCount != productIds.Count)
            return Result.Failure<DocumentResponse>(Error.NotFound("One or more products were not found."));

        var document = new DocumentHeader
        {
            DocumentNumber = DocumentHelper.GenerateDocumentNumber(),
            DocumentDate = request.DocumentDate,
            TransactionType = request.TransactionType,
            SourceWarehouseId = request.SourceWarehouseId,
            DestinationWarehouseId = request.DestinationWarehouseId,
            Notes = request.Notes,
            IsPosted = false,
            DetailLines = request.Details.Select(detail => new DocumentDetailLine
            {
                ProductItemId = detail.ProductItemId,
                Quantity = detail.Quantity,
                UserEnteredPrice = detail.UserEnteredPrice,
                FrozenCalculatedCost = 0
            }).ToList()
        };

        await _context.DocumentHeaders.AddAsync(document, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = await _context.DocumentHeaders
        .AsNoTracking()
        .Where(d => d.Id == document.Id)
        .Select(d => new DocumentResponse(
         d.Id,
         d.DocumentNumber,
         d.DocumentDate,
         d.TransactionType,
         d.SourceWarehouseId,
         d.SourceWarehouse != null ? d.SourceWarehouse.WarehouseName : "Unknown",
         d.DestinationWarehouseId,
         d.DestinationWarehouse != null ? d.DestinationWarehouse.WarehouseName : "Unknown",
         d.Notes,
         d.IsPosted,
         d.DetailLines.Select(dl => new DocumentDetailResponse(
             dl.Id,
             dl.ProductItemId,
             dl.ProductItem != null ? dl.ProductItem.NameArabic : "Unknown",
             dl.ProductItem != null ? dl.ProductItem.InternalItemCode : "Unknown",
             dl.Quantity,
             dl.UserEnteredPrice,
             dl.Quantity * dl.UserEnteredPrice
         )).ToList(),
         d.DetailLines.Sum(dl => dl.Quantity * dl.UserEnteredPrice)
     ))
        .FirstOrDefaultAsync(cancellationToken);

        if (response == null)
            return Result.Failure<DocumentResponse>(Error.NotFound("Document could not be loaded after creation."));

        return Result.Success(response);
    }



    public async Task<Result<DocumentResponse>> GetDocumentById(Guid id, CancellationToken cancellationToken)
    {
        var document = await _context.DocumentHeaders
            .AsNoTracking()
            .Include(d => d.SourceWarehouse)
            .Include(d => d.DestinationWarehouse)
            .Include(d => d.DetailLines)
                .ThenInclude(d => d.ProductItem)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (document == null)
            return Result.Failure<DocumentResponse>(Error.NotFound("Document not found"));

        return Result.Success(DocumentHelper.ToResponse(document));
    }

    public async Task<Result<PaginatedResult<DocumentResponse>>> GetDocuments(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _context.DocumentHeaders
            .AsNoTracking()
            .Include(d => d.SourceWarehouse)
            .Include(d => d.DestinationWarehouse)
            .Include(d => d.DetailLines)
                .ThenInclude(d => d.ProductItem)
            .OrderByDescending(d => d.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);
        var documents = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var items = documents.Select(DocumentHelper.ToResponse).ToList();

        return Result.Success(new PaginatedResult<DocumentResponse>(
            items,
            page,
            pageSize,
            totalCount
        ));
    }

    public async Task<Result> DeleteDocumentAsync(Guid id, CancellationToken cancellationToken)
    {
        var document = await _context.DocumentHeaders
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (document == null)
            return Result.Failure(Error.NotFound("Document not found"));

        if (document.IsPosted)
            return Result.Failure(Error.Conflict("Cannot delete a posted document"));

        document.IsDeleted = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<DocumentResponse>> UpdateDocumentAsync(
        Guid id,
        DocumentRequest request,
        CancellationToken cancellationToken)
    {
        var document = await _context.DocumentHeaders
            .Include(d => d.DetailLines)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (document == null)
            return Result.Failure<DocumentResponse>(Error.NotFound("Document not found"));

        if (document.IsPosted)
            return Result.Failure<DocumentResponse>(Error.Conflict("Cannot update a posted document"));

        document.DocumentDate = request.DocumentDate;
        document.TransactionType = request.TransactionType;
        document.Notes = request.Notes;
        document.SourceWarehouseId = request.SourceWarehouseId;
        document.DestinationWarehouseId = request.DestinationWarehouseId;

        _context.DocumentDetailLines.RemoveRange(document.DetailLines);
        document.DetailLines = request.Details.Select(detail => new DocumentDetailLine
        {
            ProductItemId = detail.ProductItemId,
            Quantity = detail.Quantity,
            UserEnteredPrice = detail.UserEnteredPrice,
            FrozenCalculatedCost = 0
        }).ToList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var documentWithDetails = await _context.DocumentHeaders
            .AsNoTracking()
            .Include(d => d.SourceWarehouse)
            .Include(d => d.DestinationWarehouse)
            .Include(d => d.DetailLines)
                .ThenInclude(d => d.ProductItem)
            .FirstOrDefaultAsync(d => d.Id == document.Id, cancellationToken);

        return Result.Success(DocumentHelper.ToResponse(documentWithDetails));
    }
}