namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.WarehouseCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(w => w.IsDeleted)
                  .HasDefaultValue(false);

            builder.HasIndex(w => new { w.WarehouseCode, w.IsDeleted })
                  .IsUnique()
                   .HasFilter("[IsDeleted] = 0");

            builder.Property(w => w.WarehouseName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(w => w.WarehouseLocation)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(w => w.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(w => w.WarehouseCode)
                .IsUnique();

            builder.HasIndex(w => w.WarehouseName);


            builder.HasMany(w => w.StockBalances)
                .WithOne(sb => sb.Warehouse)
                .HasForeignKey(sb => sb.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.FifoBatches)
                .WithOne(f => f.Warehouse)
                .HasForeignKey(f => f.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.InventoryTransactions)
                .WithOne(it => it.Warehouse)
                .HasForeignKey(it => it.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.SourceDocumentHeaders)
                .WithOne(d => d.SourceWarehouse)
                .HasForeignKey(d => d.SourceWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.DestinationDocumentHeaders)
                .WithOne(d => d.DestinationWarehouse)
                .HasForeignKey(d => d.DestinationWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}