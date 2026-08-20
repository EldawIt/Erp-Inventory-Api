

using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem.Controllers.Inventorey;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetProducts(page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.GetProductById(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts(
        [FromQuery] string? code,
        [FromQuery] string? name,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _productService.SearchProducts(code, name, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.CreateProduct(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetProductById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProduct request, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateProduct(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteProduct(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPatch("{id}/toggle-active")]
    public async Task<IActionResult> ToggleProductActive(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productService.ToggleProductActive(id, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPatch("{id}/price")]
    public async Task<IActionResult> UpdateProductPrice(
        Guid id,
        [FromBody] UpdateProductPrice request,
        CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateProductPrice(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    
}