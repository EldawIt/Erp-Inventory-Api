
 
namespace ErpSystem.Persistence.EntitiesConfigurations.Purchasing;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.Code)
            .IsUnique();

        builder.Property(s => s.Phone)
            .HasMaxLength(20);

        builder.Property(s => s.Email)
            .HasMaxLength(100);

        builder.Property(s => s.Address)
            .HasMaxLength(500);

        builder.Property(s => s.TaxNumber)
            .HasMaxLength(50);

        builder.Property(s => s.IsActive)
            .HasDefaultValue(true);
    }
}