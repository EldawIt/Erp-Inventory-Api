namespace ErpSystem.Contract.Purchasing.PurchaseOrders
{
    public record PurchaseOrderResponse(
    Guid Id,
    string OrderNumber,
    DateTime OrderDate,
    Guid SupplierId,
    string SupplierName,
    string? Notes,
    bool IsReceived,
    DateTime? ReceivedAt,
    List<PurchaseOrderDetailResponse> Details,
    decimal TotalAmount
);
}
