namespace ErpSystem.Contract.Purchasing.Suppliers
{
    public record CreateSupplier(
     string Name,
     string Code,
     string Phone,
     string? Email,
     string? Address,
     string? TaxNumber
 );
}
