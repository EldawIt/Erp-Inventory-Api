
namespace ErpSystem.Entites.Inventorey
{
    public class StockBalance : AuditableEntity
    {

        public Guid WarehouseId { get; set; }

        public Guid ProductItemId { get; set; }

        public decimal RealQuantityOnShelf { get; set; }
        public decimal SafetyReorderLevel { get; set; }
        public decimal CurrentAverageCost { get; set; }
        public decimal TotalValue { get; set; }
        public byte[]? DatabaseVersion { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public ProductItem ProductItem { get; set; } = null!;




    }
}
