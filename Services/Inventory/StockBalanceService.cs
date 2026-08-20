 

namespace ErpSystem.Services.Inventorey;

public class StockBalanceService(
    ApplicationDbContext context,
    IUnitOfWork unitOfWork
  ) : IStockBalanceService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<StockBalanceResponse>> GetStockBalance(
        Guid productId,
        Guid warehouseId,
        CancellationToken cancellationToken)
    {
        var stock = await _context.StockBalances
            .AsNoTracking()
            .Include(sb => sb.ProductItem)
            .Include(sb => sb.Warehouse)
            .FirstOrDefaultAsync(
                sb => sb.ProductItemId == productId && sb.WarehouseId == warehouseId,
                cancellationToken);

        if (stock == null)
            return Result.Failure<StockBalanceResponse>(InventoryErrors.StockBalanceNotFound);

        return Result.Success(stock.ToResponse());
    }

    public async Task<Result<PaginatedResult<StockBalanceResponse>>> GetWarehouseStock(
        Guid warehouseId,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = _context.StockBalances
            .AsNoTracking()
            .Include(sb => sb.ProductItem)
            .Include(sb => sb.Warehouse)
            .Where(sb => sb.WarehouseId == warehouseId);

        var totalCount = await query.CountAsync(cancellationToken);

        var stocks = await query
            .OrderBy(sb => sb.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var responseList = stocks.Select(sb => sb.ToResponse()).ToList();

        return Result.Success(new PaginatedResult<StockBalanceResponse>(
            responseList,
            page,
            pageSize,
            totalCount
        ));
    }

    public async Task<Result<StockBalanceResponse>> IssueStock(
        IssueStockRequest request,
        CancellationToken cancellationToken)
    {
        var stock = await _context.StockBalances
            .Include(sb => sb.ProductItem)
            .Include(sb => sb.Warehouse)
            .FirstOrDefaultAsync(
                sb => sb.ProductItemId == request.ProductItemId &&
                      sb.WarehouseId == request.WarehouseId,
                cancellationToken);

        if (stock == null)
            return Result.Failure<StockBalanceResponse>(InventoryErrors.StockBalanceNotFound);

        if (stock.RealQuantityOnShelf < request.Quantity)
            return Result.Failure<StockBalanceResponse>(InventoryErrors.InsufficientQuantity);

        decimal currentAverageCost = stock.CurrentAverageCost;

        stock.ApplyDischarge(request.Quantity);

        var transaction = StockBalanceHelper.CreateDischargeTransaction(request, currentAverageCost);

        await _context.InventoryTransactions.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(stock.ToResponse());
    }

    public async Task<Result<StockBalanceResponse>> ReceiveStock(
        ReceiveStockRequest request,
        CancellationToken cancellationToken)
    {
        var stock = await _context.StockBalances
            .Include(sb => sb.ProductItem)
            .Include(sb => sb.Warehouse)
            .FirstOrDefaultAsync(
                sb => sb.ProductItemId == request.ProductItemId &&
                      sb.WarehouseId == request.WarehouseId,
                cancellationToken);

        if (stock == null)
        {
            var productExists = await _context.ProductItems.AnyAsync(p => p.Id == request.ProductItemId, cancellationToken);
            if (!productExists) return Result.Failure<StockBalanceResponse>(InventoryErrors.ProductNotFound);

            var warehouseExists = await _context.Warehouses.AnyAsync(w => w.Id == request.WarehouseId, cancellationToken);
            if (!warehouseExists) return Result.Failure<StockBalanceResponse>(InventoryErrors.WarehouseNotFound);

            stock = StockBalanceHelper.CreateInitialStockBalance(request.ProductItemId, request.WarehouseId);
            await _context.StockBalances.AddAsync(stock, cancellationToken);
        }

        stock.ApplyAddition(request.Quantity, request.PurchasePrice);

        var transaction = StockBalanceHelper.CreateAdditionTransaction(request);
        await _context.InventoryTransactions.AddAsync(transaction, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(stock.ToResponse());
    }

    public async Task<Result<TransferStockResponse>> TransferStock(
        TransferStockRequest request,
        CancellationToken cancellationToken)
    {
        if (request.SourceWarehouseId == request.DestinationWarehouseId)
            return Result.Failure<TransferStockResponse>(
                Error.Validation("Source and destination warehouses cannot be the same."));

        var targetWarehouseIds = new[] { request.SourceWarehouseId, request.DestinationWarehouseId };

        var stocks = await _context.StockBalances
            .Include(sb => sb.ProductItem)
            .Include(sb => sb.Warehouse)
            .Where(sb => sb.ProductItemId == request.ProductItemId &&
                        targetWarehouseIds.Contains(sb.WarehouseId))
            .ToListAsync(cancellationToken);

        var sourceStock = stocks.FirstOrDefault(sb => sb.WarehouseId == request.SourceWarehouseId);
        var destStock = stocks.FirstOrDefault(sb => sb.WarehouseId == request.DestinationWarehouseId);

        if (sourceStock == null)
            return Result.Failure<TransferStockResponse>(InventoryErrors.StockBalanceNotFound);

        if (sourceStock.RealQuantityOnShelf < request.Quantity)
            return Result.Failure<TransferStockResponse>(InventoryErrors.InsufficientQuantity);

        decimal transferUnitCost = sourceStock.CurrentAverageCost;
        decimal totalTransferCost = request.Quantity * transferUnitCost;

        sourceStock.ApplyDischarge(request.Quantity);

        if (destStock == null)
        {
            var destWarehouseExists = await _context.Warehouses.AnyAsync(w => w.Id == request.DestinationWarehouseId, cancellationToken);
            if (!destWarehouseExists)
                return Result.Failure<TransferStockResponse>(InventoryErrors.WarehouseNotFound);

            destStock = StockBalanceHelper.CreateInitialStockBalance(request.ProductItemId, request.DestinationWarehouseId);
            await _context.StockBalances.AddAsync(destStock, cancellationToken);
        }

        destStock.ApplyAddition(request.Quantity, transferUnitCost);

        var (transactionSource, transactionDest) = StockBalanceHelper.CreateTransferTransactions(
            request,
            transferUnitCost,
            totalTransferCost);

        await _context.InventoryTransactions.AddRangeAsync(
            new[] { transactionSource, transactionDest },
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new TransferStockResponse(
            sourceStock.ToResponse(),
            destStock.ToResponse()
        );

        return Result.Success(response);
    }
}