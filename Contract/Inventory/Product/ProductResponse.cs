namespace ErpSystem.Contract.Inventorey.Product
{
    public record ProductResponse
(
    Guid Id,                     
    string InternalItemCode,     
    string NameArabic,           
    string NameEnglish,          
    ItemType ItemType,            
    decimal SellingPrice,         
    CalculationMethod CalculationMethod,
    bool IsActive  = true            
);
}
