namespace ErpSystem.Contract.Purchasing.PurchaseOrders
{
    public record UpdatePurchaseOrder(
     Guid SupplierId,
     DateTime OrderDate,
     string? Notes,
     List<PurchaseOrderDetailDto> Details,
     bool IsReceived,
     DateTime? ReceivedAt
 );

}
