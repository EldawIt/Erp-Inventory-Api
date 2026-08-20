public record UpdateProductPrice(
    decimal SellingPrice
);

public record UpdateProductTax(
    decimal TaxRate
);

public record UpdateProduct(
    string NameArabic,
    string NameEnglish,
    decimal SellingPrice,
    bool IsActive
);