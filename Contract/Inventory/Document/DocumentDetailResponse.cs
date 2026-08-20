namespace ErpSystem.Contract.Inventorey.Document
{
    public record DocumentDetailResponse(
    Guid Id,
    Guid? ProductItemId,
    string ProductName,
    string ProductCode,
    decimal Quantity,
    decimal UserEnteredPrice,
    decimal TotalPrice 
);
}
