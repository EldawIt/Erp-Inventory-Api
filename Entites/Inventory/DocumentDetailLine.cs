namespace ErpSystem.Entites.Inventorey
{
    public class DocumentDetailLine : AuditableEntity
    {
        public Guid? DocumentHeaderId { get; set; }
        public Guid? ProductItemId { get; set; }

        public decimal Quantity { get; set; }
        public decimal UserEnteredPrice { get; set; } 
        public decimal FrozenCalculatedCost { get; set; } 

        public decimal Price => UserEnteredPrice;
        public decimal DiscountPercentage { get; set; } = 0;
        public decimal DiscountAmount => (UserEnteredPrice * Quantity) * (DiscountPercentage / 100);
        public decimal NetPrice => (UserEnteredPrice * Quantity) - DiscountAmount;
        public ICollection<FifoBatchQueue> CreatedFifoBatches { get; set; } = [];
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];
        public DocumentHeader? DocumentHeader { get; set; }
        public ProductItem? ProductItem { get; set; }
    }
}