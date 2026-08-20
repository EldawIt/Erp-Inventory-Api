namespace ErpSystem.Services.Inventory.Interfaces
{
    public interface IProductService
    {
        Task<Result<ProductResponse>> CreateProduct(ProductRequest request, CancellationToken cancellationToken);
        Task<Result<ProductResponse>> GetProductById(Guid id, CancellationToken cancellationToken);

        Task<Result<ProductResponse>> UpdateProduct(Guid id, UpdateProduct request, CancellationToken cancellationToken);
        Task<Result> DeleteProduct(Guid id, CancellationToken cancellationToken);
        Task<Result> ToggleProductActive(Guid id, CancellationToken cancellationToken);

        Task<Result<PaginatedResult<ProductResponse>>> SearchProducts(string? code,string? name, int page1,int pageSize = 10, 
     CancellationToken cancellationToken = default);
        Task<Result<PaginatedResult<ProductResponse>>> GetProducts(
      int page = 1,
      int pageSize = 20,
      CancellationToken cancellationToken = default);
        Task<Result<ProductResponse>> UpdateProductPrice(Guid id, UpdateProductPrice request, CancellationToken cancellationToken);
         

    }
}