namespace ErpSystem.Entites.Purchasing
{
    public class PurchaseOrderDetail : AuditableEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid ProductItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal PurchasePrice { get; set; } 
        public decimal TotalPrice => Quantity * PurchasePrice; 

        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        public ProductItem ProductItem { get; set; } = null!;
    }
}
