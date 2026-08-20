
namespace ErpSystem.Entites.Inventorey
{
    public class ProductItem:AuditableEntity
    {
        public string InternalItemCode { get; set; } = null!;
        public string NameArabic { get; set; } = null!;
        public string NameEnglish { get; set; } = null!;
        public ItemType ItemType { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal TaxRate { get; set; } = 0.14m;
        public bool IsActive { get; set; } = true;
        public CalculationMethod CalculationMethod { get; set; }

        public ICollection<StockBalance> StockBalances { get; set; } = [];
        public ICollection<DocumentDetailLine> DocumentLines { get; set; } = [];
        public ICollection<FifoBatchQueue> FifoBatches { get; set; } = [];
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];
    }
}
