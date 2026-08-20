


using ErpSystem.Services.Inventory.Interfaces;
using System.Diagnostics;

namespace ErpSystem.Services.Inventorey
{
    public class ProductServices(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;
        //public async Task<Result<ProductResponse>> CreateProduct(ProductRequest request, CancellationToken cancellationToken)
        //{
        //    var productIsExisting = await _context.ProductItems.AnyAsync(x => x.InternalItemCode == request.InternalItemCode, cancellationToken);
        //    if (productIsExisting)
        //        return Result.Failure<ProductResponse>(InventoryErrors.ProductCodeAlreadyExists);

        //    var product = request.Adapt<ProductItem>();
        //    product.CalculationMethod = CalculationMethod.WeightedAverage;
        //    product.ItemType = ItemType.StockableItem;
        //    product.TaxRate = 14;
        //    await _context.ProductItems.AddAsync(product);
        //    await _context.SaveChangesAsync(cancellationToken);

        //    var response = product.Adapt<ProductResponse>();
        //    return Result.Success(response);

        //}
        public async Task<Result<ProductResponse>> CreateProduct(ProductRequest request, CancellationToken cancellationToken)
        {
            var productIsExisting = await _context.ProductItems
                .AnyAsync(x => x.InternalItemCode == request.InternalItemCode, cancellationToken);

            if (productIsExisting)
                return Result.Failure<ProductResponse>(InventoryErrors.ProductCodeAlreadyExists);

            var product = request.Adapt<ProductItem>();

            product.ItemType = ItemType.StockableItem;
            product.CalculationMethod = CalculationMethod.WeightedAverage;
            product.TaxRate = 0.14m;
            product.IsActive = true;

           

            await _context.ProductItems.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = product.Adapt<ProductResponse>();
            return Result.Success(response);
        }

        public async Task<Result> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.ProductItems
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return Result.Failure(InventoryErrors.ProductNotFound);

            var hasStock = await _context.StockBalances
                .AnyAsync(sb => sb.ProductItemId == id && sb.RealQuantityOnShelf > 0, cancellationToken);

            if (hasStock)
                return Result.Failure(InventoryErrors.HasQuantityProduct);

            product.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }



        public async Task<Result<ProductResponse>> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.ProductItems
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return Result.Failure<ProductResponse>(InventoryErrors.ProductNotFound);

            var response = product.Adapt<ProductResponse>();
            return Result.Success(response);

        }
        public async Task<Result<PaginatedResult<ProductResponse>>> GetProducts(
     int page = 1,
     int pageSize = 10,
     CancellationToken cancellationToken = default)
        {

            var query = _context.ProductItems
                 .AsNoTracking()
                .Where(p => !p.IsDeleted)
                 .OrderBy(p => p.Id);
            var totalCount = await query.CountAsync(cancellationToken);
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectToType<ProductResponse>()
                .ToListAsync(cancellationToken);
            var result = new PaginatedResult<ProductResponse>(
                products,
                page,
                pageSize,
                totalCount
            );

            return Result.Success(result);
        }

        public async Task<Result> ToggleProductActive(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.ProductItems
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return Result.Failure(InventoryErrors.ProductNotFound);

            product.IsActive = !product.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<ProductResponse>> UpdateProduct(
    Guid id,
    UpdateProduct request,
    CancellationToken cancellationToken)
        {
            var product = await _context.ProductItems
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return Result.Failure<ProductResponse>(InventoryErrors.ProductNotFound);

            request.Adapt(product);



            await _context.SaveChangesAsync(cancellationToken);

            var response = product.Adapt<ProductResponse>();
            return Result.Success(response);
        }
        public async Task<Result<PaginatedResult<ProductResponse>>> SearchProducts(
     string? code,
     string? name,
     int page = 1,
     int pageSize = 10,
     CancellationToken cancellationToken = default)
        {
            var query = _context.ProductItems
                .Where(p => !p.IsDeleted)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(p => p.InternalItemCode.Contains(code.Trim()));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p =>
                    p.NameArabic.Contains(name.Trim()) ||
                    p.NameEnglish.Contains(name.Trim()));

            query = query.OrderBy(p => p.Id);

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectToType<ProductResponse>()
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<ProductResponse>(
                products,
                page,
                pageSize,
                totalCount
            );

            return Result.Success(result);
        }

        public async Task<Result<ProductResponse>> UpdateProductPrice(
            Guid id,
            UpdateProductPrice request,
            CancellationToken cancellationToken)
        {
            var product = await _context.ProductItems
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
                return Result.Failure<ProductResponse>(InventoryErrors.ProductNotFound);

            product.SellingPrice = request.SellingPrice;
            await _context.SaveChangesAsync(cancellationToken);

            var response = product.Adapt<ProductResponse>();
            return Result.Success(response);
        }


    }
}
