namespace ErpSystem.Entites.Purchasing
{
    public class Supplier:AuditableEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string TaxNumber { get; set; } = null!; 
        public bool IsActive { get; set; } = true;

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = [];
    }
}
