namespace ErpSystem.Contract.Inventorey.StockBalnce
{
    public record IssueStockRequest(
    Guid ProductItemId,
    Guid WarehouseId,
    decimal Quantity
);
}
