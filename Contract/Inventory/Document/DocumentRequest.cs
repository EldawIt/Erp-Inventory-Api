
namespace ErpSystem.Contract.Inventorey.Document
{
    public record DocumentRequest(
     DateTime DocumentDate,
     TransactionType TransactionType,
     Guid? WarehouseId, 
     Guid? SourceWarehouseId, 
     Guid? DestinationWarehouseId, 
     string? Notes,
     List<DocumentDetailRequest> Details
 );
}
