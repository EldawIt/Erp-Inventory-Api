namespace ErpSystem.Contract.Inventorey.Document
{
    public record DocumentResponse(
    Guid Id,
    string DocumentNumber,
    DateTime DocumentDate,
    TransactionType TransactionType,
    Guid? SourceWarehouseId,
    string? SourceWarehouseName,
    Guid? DestinationWarehouseId,
    string? DestinationWarehouseName,
    string? Notes,
    bool IsPosted,
    List<DocumentDetailResponse> Details,
    decimal TotalAmount
);
}
