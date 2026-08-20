
namespace ErpSystem.Persistence.EntitiesConfigurations.Purchasing;

public class PurchaseOrderDetailConfiguration : IEntityTypeConfiguration<PurchaseOrderDetail>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderDetail> builder)
    {
        builder.ToTable("PurchaseOrderDetails");

        builder.HasKey(pod => pod.Id);

        builder.Property(pod => pod.Quantity)
            .IsRequired()
            .HasColumnType("decimal(18,3)");

        builder.Property(pod => pod.PurchasePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(pod => pod.PurchaseOrder)
            .WithMany(po => po.Details)
            .HasForeignKey(pod => pod.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pod => pod.ProductItem)
            .WithMany()
            .HasForeignKey(pod => pod.ProductItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}