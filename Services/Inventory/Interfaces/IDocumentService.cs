namespace ErpSystem.Services.Inventory.Interfaces
{
    public interface IDocumentService
    {
        Task<Result<DocumentResponse>> CreateDocument(DocumentRequest request, CancellationToken cancellationToken);
        Task<Result<DocumentResponse>> GetDocumentById(Guid id, CancellationToken cancellationToken);
        Task<Result<PaginatedResult<DocumentResponse>>> GetDocuments(int page, int pageSize, CancellationToken cancellationToken);
        Task<Result> DeleteDocumentAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<DocumentResponse>> UpdateDocumentAsync(Guid id,  DocumentRequest request, CancellationToken cancellationToken);
    }
}