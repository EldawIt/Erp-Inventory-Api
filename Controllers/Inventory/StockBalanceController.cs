

using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem.Controllers.Inventorey;

[Route("api/[controller]")]
[ApiController]
public class StockBalanceController (IStockBalanceService stockBalanceService) : ControllerBase
{
    private readonly IStockBalanceService _stockBalanceService = stockBalanceService;

    

    [HttpGet("{productId}/{warehouseId}")]
    public async Task<IActionResult> GetStockBalance(
        Guid productId,
        Guid warehouseId,
        CancellationToken cancellationToken)
    {
        var result = await _stockBalanceService.GetStockBalance(productId, warehouseId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("warehouse/{warehouseId}")]
    public async Task<IActionResult> GetWarehouseStock(
        Guid warehouseId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _stockBalanceService.GetWarehouseStock(warehouseId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("receive")]
    public async Task<IActionResult> ReceiveStock(
        [FromBody] ReceiveStockRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _stockBalanceService.ReceiveStock(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("issue")]
    public async Task<IActionResult> IssueStock(
        [FromBody] IssueStockRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _stockBalanceService.IssueStock(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferStock(
        [FromBody] TransferStockRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _stockBalanceService.TransferStock(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}