namespace ErpSystem.Contract.Purchasing.Suppliers
{
    public record SupplierResponse(
     Guid Id,
     string Name,
     string Code,
     string Phone,
     string? Email,
     string? Address,
     string? TaxNumber,
     bool IsActive
 );
}
