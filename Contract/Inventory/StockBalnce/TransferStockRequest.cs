namespace ErpSystem.Contract.Inventorey.StockBalnce
{
    public record TransferStockRequest(
    Guid ProductItemId,
    Guid SourceWarehouseId,
    Guid DestinationWarehouseId,
    decimal Quantity
);
}
