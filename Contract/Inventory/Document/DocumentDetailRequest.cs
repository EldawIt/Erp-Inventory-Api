namespace ErpSystem.Contract.Inventorey.Document
{
    public record DocumentDetailRequest(
     Guid ProductItemId,
    decimal Quantity,
    decimal UserEnteredPrice
 );

}
