namespace ErpSystem.Entites.Inventorey
{
    public class FifoBatchQueue:AuditableEntity
    {


        public Guid ProductItemId { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid SourceDocumentLineId { get; set; }

        public decimal OriginalIncomingQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal BatchCostPrice { get; set; }
        public DateTime ArrivalDate { get; set; }
        public ProductItem ProductItem { get; set; } = null!;
        public DocumentDetailLine SourceDocumentLine { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;



    }
}
