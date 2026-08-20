namespace ErpSystem.Contract.Purchasing.PurchaseOrders
{
    public record CreatePurchaseOrder(
    Guid SupplierId,
    DateTime OrderDate,
    string? Notes,
    List<PurchaseOrderDetailDto> Details
);
}
