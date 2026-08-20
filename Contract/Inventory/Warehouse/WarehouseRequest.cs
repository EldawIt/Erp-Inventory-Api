namespace ErpSystem.Contract.Inventorey.Warehouse
{
    public record WarehouseRequest
    (
      string WarehouseCode,
      string WarehouseName,
      string WarehouseLocation,
      bool IsActive  
     );
       
     
}
