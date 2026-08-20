
using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem.Controllers.Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController(IDocumentService documentService, IDocumentPostingService postingService) : ControllerBase
    {
        private readonly IDocumentService _documentService = documentService;
        private readonly IDocumentPostingService _postingService = postingService;

        
        [HttpPost]
        public async Task<IActionResult> CreateDocument(
        [FromBody] DocumentRequest request,
        CancellationToken cancellationToken)
        {
            var result = await _documentService.CreateDocument(request, cancellationToken);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetDocumentById), new { id = result.Value.Id }, result.Value);

            return result.ToProblem();
        }

        [HttpPost("{id}/post")]
        public async Task<IActionResult> PostDocument(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _postingService.PostDocumentAsync(id, cancellationToken);

            if (result.IsSuccess)
                return Ok(new { message = "Document posted successfully" });

            return result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _documentService.GetDocumentById(id, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.ToProblem();
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var result = await _documentService.GetDocuments(page, pageSize, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return result.ToProblem();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id,CancellationToken cancellationToken)
        {
            var result = await _documentService.DeleteDocumentAsync(id, cancellationToken);

            if (result.IsSuccess)
                return NoContent();

            return result.ToProblem();
        }
    }
}
