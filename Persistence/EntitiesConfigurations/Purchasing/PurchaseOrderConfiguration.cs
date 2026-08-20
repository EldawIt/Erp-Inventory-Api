namespace ErpSystem.Persistence.EntitiesConfigurations.Purchasing;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");

        builder.HasKey(po => po.Id);

        builder.Property(po => po.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(po => po.OrderNumber)
            .IsUnique();

        builder.Property(po => po.OrderDate)
            .IsRequired();

        builder.Property(po => po.Notes)
            .HasMaxLength(500);

        builder.Property(po => po.IsReceived)
            .HasDefaultValue(false);

        builder.HasOne(po => po.Supplier)
            .WithMany(s => s.PurchaseOrders)
            .HasForeignKey(po => po.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(po => po.Details)
            .WithOne(d => d.PurchaseOrder)
            .HasForeignKey(d => d.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}