namespace ErpSystem.Contract.Inventorey.StockBalnce
{
    public record StockBalanceResponse(
     Guid Id,
     Guid ProductItemId,
     string ProductName,
     string ProductCode,
     Guid WarehouseId,
     string WarehouseName,
     decimal RealQuantityOnShelf,
     decimal SafetyReorderLevel,
     decimal CurrentAverageCost,
     decimal TotalValue

 );
}
