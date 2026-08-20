using ErpSystem.Contract.Inventorey.Warehouse;
using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem.Controllers.Inventorey;

[ApiController]
[Route("[controller]")]
public class WarehouseController(IWarehouseService warehouseService) : ControllerBase
{
    private readonly IWarehouseService _warehouseService = warehouseService;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _warehouseService.GetAllWarehouses(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _warehouseService.GetWarehouseById(id, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WarehouseRequest request, CancellationToken cancellationToken)
    {
        var result = await _warehouseService.CreateWarehouse(request, cancellationToken);

        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);

        return result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWarehouse request, CancellationToken cancellationToken)
    {
        var result = await _warehouseService.UpdateWarehouse(id, request, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _warehouseService.DeleteWarehouse(id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPatch("{id}/toggle-active")]
    public async Task<IActionResult> ToggleActive(Guid id, CancellationToken cancellationToken)
    {
        var result = await _warehouseService.ToggleWarehouseActive(id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}