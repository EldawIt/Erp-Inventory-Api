namespace ErpSystem.Contract.Purchasing.PurchaseOrders
{
    public record PurchaseOrderDetailResponse(
     Guid Id,
     Guid ProductItemId,
     string ProductName,
     string ProductCode,
     decimal Quantity,
     decimal PurchasePrice,
     decimal TotalPrice
 );
}
