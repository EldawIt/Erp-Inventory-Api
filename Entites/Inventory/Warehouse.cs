namespace ErpSystem.Entites.Inventorey
{
    public class Warehouse: AuditableEntity
    {
        public string WarehouseCode { get; set; } = null!;// important to search
        public string WarehouseName { get; set; } = null!;
        public string WarehouseLocation { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public ICollection<StockBalance> StockBalances { get; set; } = [];
        public ICollection<FifoBatchQueue> FifoBatches { get; set; } = [];
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];
        public ICollection<DocumentHeader> SourceDocumentHeaders { get; set; } = [];
        public ICollection<DocumentHeader> DestinationDocumentHeaders { get; set; } = [];
    }
}
