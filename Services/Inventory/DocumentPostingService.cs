using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem.Services.Inventory;

public class DocumentPostingService(ApplicationDbContext context, IUnitOfWork unitOfWork) : IDocumentPostingService
{
    private readonly ApplicationDbContext _context =context;
    private readonly IUnitOfWork _unitOfWork=unitOfWork;

    

    public async Task<Result> PostDocumentAsync(Guid documentId, CancellationToken cancellationToken)
    {
        var document = await LoadDocumentWithLinesAsync(documentId, cancellationToken);
        if (document == null)
            return Result.Failure(Error.NotFound("Document not found"));

        if (document.IsPosted)
            return Result.Failure(Error.Conflict("Document already posted"));

        var validLines = GetValidLines(document);
        if (!validLines.Any())
            return Result.Failure(Error.BadRequest("Document has no valid lines"));

        var stockBalances = await LoadStockBalancesAsync(document, validLines, cancellationToken);

        var transactions = new List<InventoryTransaction>();

        foreach (var detail in validLines)
        {
            var productId = detail.ProductItemId!.Value;

            var result = document.TransactionType switch
            {
                TransactionType.Discharge => DischargeStock(document, detail, productId, stockBalances, transactions),
                TransactionType.Addition => AddStock(document, detail, productId, stockBalances, transactions),
                TransactionType.Transfer => TransferStock(document, detail, productId, stockBalances, transactions),
                _ => Result.Failure(Error.BadRequest("Invalid transaction type"))
            };

            if (result.IsFailure)
                return result;
        }

        if (transactions.Any())
            await _context.InventoryTransactions.AddRangeAsync(transactions, cancellationToken);

        document.IsPosted = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    #region Private Methods

    private async Task<DocumentHeader?> LoadDocumentWithLinesAsync(Guid id, CancellationToken ct)
        => await _context.DocumentHeaders
            .Include(d => d.DetailLines)
            .FirstOrDefaultAsync(d => d.Id == id, ct);

    private List<DocumentDetailLine> GetValidLines(DocumentHeader document)
        => document.DetailLines
            .Where(d => d.ProductItemId.HasValue && d.Quantity > 0)
            .ToList();

    private async Task<List<StockBalance>> LoadStockBalancesAsync(DocumentHeader doc, List<DocumentDetailLine> lines, CancellationToken ct)
    {
        var productIds = lines.Select(l => l.ProductItemId!.Value).Distinct().ToList();
        var warehouseIds = new[] { doc.SourceWarehouseId, doc.DestinationWarehouseId }
            .Where(w => w.HasValue).Select(w => w!.Value).Distinct().ToList();

        return await _context.StockBalances
            .Where(sb => warehouseIds.Contains(sb.WarehouseId) && productIds.Contains(sb.ProductItemId))
            .ToListAsync(ct);
    }

    private Result DischargeStock(DocumentHeader doc, DocumentDetailLine detail, Guid productId, List<StockBalance> stockBalances, List<InventoryTransaction> transactions)
    {
        var stock = FindStock(stockBalances, doc.SourceWarehouseId, productId);

        if (stock == null || stock.RealQuantityOnShelf < detail.Quantity)
            return Result.Failure(Error.BadRequest($"Insufficient stock for product: {productId}"));

        stock.ApplyDischarge(detail.Quantity);

        transactions.Add(StockBalanceHelper.CreateTransaction(
            productId,
            doc.SourceWarehouseId!.Value,
            detail,
            stock.CurrentAverageCost,
            TransactionType.Discharge,
            doc.Id));

        return Result.Success();
    }

    private Result AddStock(DocumentHeader doc, DocumentDetailLine detail, Guid productId, List<StockBalance> stockBalances, List<InventoryTransaction> transactions)
    {
        var stock = GetOrCreateStock(stockBalances, doc.DestinationWarehouseId, productId);

        stock.ApplyAddition(detail.Quantity, detail.UserEnteredPrice);

        transactions.Add(StockBalanceHelper.CreateTransaction(
            productId,
            doc.DestinationWarehouseId!.Value,
            detail,
            detail.UserEnteredPrice,
            TransactionType.Addition,
            doc.Id));

        return Result.Success();
    }

    private Result TransferStock(DocumentHeader doc, DocumentDetailLine detail, Guid productId, List<StockBalance> stockBalances, List<InventoryTransaction> transactions)
    {
        var sourceStock = FindStock(stockBalances, doc.SourceWarehouseId, productId);

        if (sourceStock == null || sourceStock.RealQuantityOnShelf < detail.Quantity)
            return Result.Failure(Error.BadRequest($"Insufficient stock for product: {productId}"));

        var destStock = GetOrCreateStock(stockBalances, doc.DestinationWarehouseId, productId);

        sourceStock.ApplyDischarge(detail.Quantity);
        destStock.ApplyAddition(detail.Quantity, sourceStock.CurrentAverageCost);

        transactions.Add(StockBalanceHelper.CreateTransaction(
            productId,
            doc.SourceWarehouseId!.Value,
            detail,
            sourceStock.CurrentAverageCost,
            TransactionType.Transfer,
            doc.Id));

        transactions.Add(StockBalanceHelper.CreateTransaction(
            productId,
            doc.DestinationWarehouseId!.Value,
            detail,
            sourceStock.CurrentAverageCost,
            TransactionType.Transfer,
            doc.Id));

        return Result.Success();
    }

    private StockBalance? FindStock(List<StockBalance> stockBalances, Guid? warehouseId, Guid productId)
        => stockBalances.FirstOrDefault(sb => sb.WarehouseId == warehouseId && sb.ProductItemId == productId);

    private StockBalance GetOrCreateStock(List<StockBalance> stockBalances, Guid? warehouseId, Guid productId)
    {
        var stock = FindStock(stockBalances, warehouseId, productId);

        if (stock == null)
        {
            stock = StockBalanceHelper.CreateInitialStockBalance(productId, warehouseId!.Value);
            _context.StockBalances.Add(stock);
            stockBalances.Add(stock);
        }

        return stock;
    }

    #endregion
}