namespace ErpSystem.Helpers;

public static class DocumentHelper
{
    public static string GenerateDocumentNumber()
        => $"DOC-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N}".Substring(0, 20);

    public static Result ValidateDocument(DocumentRequest request)
    {
        if (request.Details == null || request.Details.Count == 0)
            return Result.Failure(Error.BadRequest("Document must have at least one detail line"));

        if (request.TransactionType == TransactionType.Addition && !request.DestinationWarehouseId.HasValue)
            return Result.Failure(Error.BadRequest("Destination warehouse is required for addition"));

        if (request.TransactionType == TransactionType.Discharge && !request.SourceWarehouseId.HasValue)
            return Result.Failure(Error.BadRequest("Source warehouse is required for discharge"));

        if (request.TransactionType == TransactionType.Transfer)
        {
            if (!request.SourceWarehouseId.HasValue)
                return Result.Failure(Error.BadRequest("Source warehouse is required for transfer"));

            if (!request.DestinationWarehouseId.HasValue)
                return Result.Failure(Error.BadRequest("Destination warehouse is required for transfer"));

            if (request.SourceWarehouseId == request.DestinationWarehouseId)
                return Result.Failure(Error.BadRequest("Source and destination warehouses cannot be the same"));
        }

        return Result.Success();
    }

    public static DocumentResponse ToResponse(DocumentHeader document)
    {
        return new DocumentResponse(
            document.Id,
            document.DocumentNumber,
            document.DocumentDate,
            document.TransactionType,
            document.SourceWarehouseId,
            document.SourceWarehouse?.WarehouseName ?? "Unknown",
            document.DestinationWarehouseId,
            document.DestinationWarehouse?.WarehouseName ?? "Unknown",
            document.Notes,
            document.IsPosted,
            document.DetailLines?.Select(d => new DocumentDetailResponse(
                d.Id,
                d.ProductItemId,
                d.ProductItem?.NameArabic ?? "Unknown",
                d.ProductItem?.InternalItemCode ?? "Unknown",
                d.Quantity,
                d.UserEnteredPrice,
                d.Quantity * d.UserEnteredPrice
            )).ToList() ?? new List<DocumentDetailResponse>(),
            document.DetailLines?.Sum(d => d.Quantity * d.UserEnteredPrice) ?? 0
        );
    }
}