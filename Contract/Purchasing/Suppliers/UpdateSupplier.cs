namespace ErpSystem.Contract.Purchasing.Suppliers
{
    public record UpdateSupplier(
     string Name,
     string Phone,
     string? Email,
     string? Address,
     string? TaxNumber,
     bool IsActive
 );
}
