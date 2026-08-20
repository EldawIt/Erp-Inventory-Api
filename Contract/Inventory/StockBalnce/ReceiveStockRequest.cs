namespace ErpSystem.Contract.Inventorey.StockBalnce
{
    public record ReceiveStockRequest(
    Guid ProductItemId,
    Guid WarehouseId,
    decimal Quantity,
    decimal PurchasePrice
);
}
