namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey
{
    public class FifoBatchQueueConfigration : IEntityTypeConfiguration<FifoBatchQueue>
    {
        public void Configure(EntityTypeBuilder<FifoBatchQueue> builder)
        {
            builder.ToTable("FifoBatchQueues");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.OriginalIncomingQuantity)
                .IsRequired()
                .HasColumnType("decimal(18,3)");

            builder.Property(f => f.RemainingQuantity)
                .IsRequired()
                .HasColumnType("decimal(18,3)");

            builder.Property(f => f.BatchCostPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(f => f.ArrivalDate)
                .IsRequired();

            builder.HasIndex(f => new { f.WarehouseId, f.ProductItemId })
                .HasDatabaseName("IX_FifoBatchQueue_Warehouse_Product");

            builder.HasIndex(f => f.ArrivalDate)
                .HasDatabaseName("IX_FifoBatchQueue_ArrivalDate");


            builder.HasOne(f => f.ProductItem)
                .WithMany()  
                .HasForeignKey(f => f.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Warehouse)
                .WithMany(w => w.FifoBatches)  
                .HasForeignKey(f => f.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.SourceDocumentLine)
                .WithMany(d => d.CreatedFifoBatches)  
                .HasForeignKey(f => f.SourceDocumentLineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
