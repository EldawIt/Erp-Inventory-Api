namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey;

public class DocumentHeaderConfiguration : IEntityTypeConfiguration<DocumentHeader>
{
    public void Configure(EntityTypeBuilder<DocumentHeader> builder)
    {
        builder.ToTable("DocumentHeaders");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.DocumentNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(h => h.DocumentNumber)
            .IsUnique();

        builder.Property(h => h.DocumentDate)
            .IsRequired();

        builder.Property(h => h.TransactionType)
            .IsRequired();

        builder.Property(h => h.Notes)
            .HasMaxLength(500)
            .IsRequired(false);


        builder.HasOne(h => h.SourceWarehouse)
            .WithMany()
            .HasForeignKey(h => h.SourceWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.DestinationWarehouse)
            .WithMany()
            .HasForeignKey(h => h.DestinationWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(h => h.DetailLines)
            .WithOne(l => l.DocumentHeader)
            .HasForeignKey(l => l.DocumentHeaderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(h => h.InventoryTransactions)
            .WithOne(it => it.DocumentHeader)
            .HasForeignKey(t => t.DocumentHeaderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}