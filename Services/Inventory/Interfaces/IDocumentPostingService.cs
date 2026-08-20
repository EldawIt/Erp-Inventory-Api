namespace ErpSystem.Services.Inventory.Interfaces
{
    public interface IDocumentPostingService
    {
        Task<Result> PostDocumentAsync(
       Guid documentId,
       CancellationToken cancellationToken);
    }
}
