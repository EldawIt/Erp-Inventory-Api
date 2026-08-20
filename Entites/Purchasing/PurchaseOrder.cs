namespace ErpSystem.Entites.Purchasing
{
    public class PurchaseOrder : AuditableEntity
    {
        public string OrderNumber { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public Guid SupplierId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool IsReceived { get; set; } = false;
        public DateTime? ReceivedAt { get; set; }

        public Supplier Supplier { get; set; } = null!;
        public ICollection<PurchaseOrderDetail> Details { get; set; } = [];
    }
}
