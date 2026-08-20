namespace ErpSystem.Services.Inventory.Interfaces
{
    public interface IWarehouseService
    {

        Task<Result<WarehouseResponse>> CreateWarehouse(WarehouseRequest dto, CancellationToken cancellationToken);
        Task<Result<WarehouseResponse>> UpdateWarehouse(Guid id, UpdateWarehouse dto, CancellationToken cancellationToken);
        Task<Result> DeleteWarehouse(Guid id, CancellationToken cancellationToken);
        Task<Result> ToggleWarehouseActive(Guid id, CancellationToken cancellationToken);
        Task<Result<List<WarehouseResponse>>> GetAllWarehouses(CancellationToken cancellationToken);
        Task<Result<WarehouseResponse>> GetWarehouseById(Guid id, CancellationToken cancellationToken);
    }
}
