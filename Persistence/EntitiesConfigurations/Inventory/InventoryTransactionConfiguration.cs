namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey
{
    public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransactions");

            builder.HasKey(it => it.Id);

            builder.Property(it => it.TransactionDate)
                .IsRequired();

            builder.Property(it => it.TransactionType)
                .IsRequired();

            builder.Property(it => it.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,3)");

            builder.Property(it => it.PriceDuringTransaction)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(it => it.TotalCost)
    .IsRequired()
    .HasColumnType("decimal(18,2)");

            builder.HasIndex(it => it.TransactionDate)
                .HasDatabaseName("IX_InventoryTransaction_Date");

            builder.HasIndex(it => new { it.WarehouseId, it.ProductItemId })
                .HasDatabaseName("IX_InventoryTransaction_Warehouse_Product");

            builder.HasIndex(it => it.DocumentHeaderId)
                .HasDatabaseName("IX_InventoryTransaction_DocumentHeader");

            builder.HasOne(it => it.DocumentDetailLine)
                .WithMany(d => d.InventoryTransactions)
                .HasForeignKey(it => it.DocumentDetailLineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(it => it.DocumentHeader)
                .WithMany(d => d.InventoryTransactions)
                .HasForeignKey(it => it.DocumentHeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(it => it.Warehouse)
                .WithMany(w => w.InventoryTransactions)
                .HasForeignKey(it => it.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(it => it.ProductItem)
                .WithMany(p => p.InventoryTransactions)
                .HasForeignKey(it => it.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}