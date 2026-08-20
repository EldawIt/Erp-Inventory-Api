namespace ErpSystem.Contract.Purchasing.PurchaseOrders
{
    public record PurchaseOrderDetailDto(
    Guid ProductItemId,
    decimal Quantity,
    decimal PurchasePrice
);
}
