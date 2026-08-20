
using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem.Services.Inventorey
{
    public class WarehouseService(ApplicationDbContext context) : IWarehouseService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<WarehouseResponse>> CreateWarehouse(WarehouseRequest dto, CancellationToken cancellationToken)
        {
            var exists = await _context.Warehouses
                .AnyAsync(w => w.WarehouseCode == dto.WarehouseCode, cancellationToken);

            if (exists)
                return Result.Failure<WarehouseResponse>(InventoryErrors.WarehouseCodeAlreadyExists);
            var warehouse = dto.Adapt<Warehouse>();

            await _context.Warehouses.AddAsync(warehouse, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = warehouse.Adapt<WarehouseResponse>();

            return Result.Success(response);
        }

        public async Task<Result<List<WarehouseResponse>>> GetAllWarehouses(CancellationToken cancellationToken)
        {
            var warehouses = await _context.Warehouses
                 .Where(w => !w.IsDeleted)
                  .AsNoTracking()
                .Select(w => new WarehouseResponse(
                    w.Id,
                    w.WarehouseCode,
                    w.WarehouseName,
                    w.WarehouseLocation,
                    w.IsActive
                ))
                .ToListAsync(cancellationToken);

            return Result.Success(warehouses);
        }

        public async Task<Result<WarehouseResponse>> GetWarehouseById(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

            if (warehouse == null)
                return Result.Failure<WarehouseResponse>(InventoryErrors.WarehouseNotFound);

            var response = new WarehouseResponse(
                warehouse.Id,
                warehouse.WarehouseCode,
                warehouse.WarehouseName,
                warehouse.WarehouseLocation,
                warehouse.IsActive
            );

            return Result.Success(response);
        }

        public async Task<Result<WarehouseResponse>> UpdateWarehouse(Guid id, UpdateWarehouse dto, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

            if (warehouse == null)
                return Result.Failure<WarehouseResponse>(InventoryErrors.WarehouseNotFound);

            var duplicate = await _context.Warehouses
                .AnyAsync(w => w.WarehouseCode == dto.WarehouseCode && w.Id != id, cancellationToken);

            if (duplicate)
                return Result.Failure<WarehouseResponse>(InventoryErrors.WarehouseCodeAlreadyExists);

            dto.Adapt(warehouse);

            await _context.SaveChangesAsync(cancellationToken);

            var response = warehouse.Adapt<WarehouseResponse>();

            return Result.Success(response);
        }

        public async Task<Result> DeleteWarehouse(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

            if (warehouse == null)
                return Result.Failure(InventoryErrors.WarehouseNotFound);

            var hasStock = await _context.StockBalances
                .AnyAsync(sb => sb.WarehouseId == id, cancellationToken);

            if (hasStock)
                return Result.Failure(InventoryErrors.WarehouseHasStockBalances);

            warehouse.IsDeleted = true;
            warehouse.IsActive = false;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ToggleWarehouseActive(Guid id, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

            if (warehouse == null)
                return Result.Failure(InventoryErrors.WarehouseNotFound);

            warehouse.IsActive = !warehouse.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}