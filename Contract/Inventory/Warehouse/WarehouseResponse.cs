namespace ErpSystem.Contract.Inventorey.Warehouse
{
    public record WarehouseResponse
     (
         Guid Id,
         string WarehouseCode,
         string WarehouseName,
         string WarehouseLocation,
         bool IsActive
     );
}
