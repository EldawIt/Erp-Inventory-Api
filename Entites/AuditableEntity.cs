namespace ErpSystem.Entites
{
    public  class AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? ModifiedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }
    }
}
