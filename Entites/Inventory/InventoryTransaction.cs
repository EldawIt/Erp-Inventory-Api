namespace ErpSystem.Entites.Inventorey
{
    public class InventoryTransaction : AuditableEntity
    {
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }

        public Guid? DocumentDetailLineId { get; set; }
        public Guid? DocumentHeaderId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid ProductItemId { get; set; }

        public decimal Quantity { get; set; }
        public decimal PriceDuringTransaction { get; set; }
        public decimal TotalCost { get; set; }

        public DocumentDetailLine? DocumentDetailLine { get; set; } = null!;
        public DocumentHeader? DocumentHeader { get; set; } = null!;  
        public Warehouse Warehouse { get; set; } = null!;
        public ProductItem ProductItem { get; set; } = null!;
    }
}