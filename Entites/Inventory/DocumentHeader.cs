namespace ErpSystem.Entites.Inventorey
{
    public class DocumentHeader : AuditableEntity
    {
        public string DocumentNumber { get; set; } = null!;
        public DateTime DocumentDate { get; set; }
        public TransactionType TransactionType { get; set; }

        public bool IsPosted { get; set; } = false; 
        public Guid? SourceWarehouseId { get; set; }
        public Guid? DestinationWarehouseId { get; set; }
        public string? Notes { get; set; }
        public string? PostedBy { get; set; }
        public DateTime? PostedAt { get; set; }

        public ICollection<DocumentDetailLine> DetailLines { get; set; } = [];
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];
        public Warehouse? SourceWarehouse { get; set; }
        public Warehouse? DestinationWarehouse { get; set; }
    }
}