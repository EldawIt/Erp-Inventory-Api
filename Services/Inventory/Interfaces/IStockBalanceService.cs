namespace ErpSystem.Services.Inventory.Interfaces
{
    public interface IStockBalanceService
    {
        Task<Result<StockBalanceResponse>> GetStockBalance(Guid productId, Guid warehouseId, CancellationToken cancellationToken);
        public   Task<Result<PaginatedResult<StockBalanceResponse>>> GetWarehouseStock(
         Guid warehouseId,
         int page = 1,
         int pageSize = 20,
         CancellationToken cancellationToken = default);
        Task<Result<StockBalanceResponse>> ReceiveStock(ReceiveStockRequest request, CancellationToken cancellationToken);
        Task<Result<StockBalanceResponse>> IssueStock(IssueStockRequest request, CancellationToken cancellationToken);
        Task<Result<TransferStockResponse>> TransferStock(
         TransferStockRequest request,
         CancellationToken cancellationToken
     );
    }
}
