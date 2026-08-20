namespace ErpSystem.Contract.Inventorey.Warehouse
{
    public record UpdateWarehouse
    (
        string WarehouseCode,
    string WarehouseName,
    string WarehouseLocation,
    bool IsActive
        );
}
