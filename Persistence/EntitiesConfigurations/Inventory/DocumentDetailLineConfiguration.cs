namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey
{
    public class DocumentDetailLineConfiguration : IEntityTypeConfiguration<DocumentDetailLine>
    {
        public void Configure(EntityTypeBuilder<DocumentDetailLine> builder)
        {
            builder.ToTable("DocumentDetailLines");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18, 3)"); 

            builder.Property(l => l.UserEnteredPrice)
                .IsRequired()
                .HasColumnType("decimal(18, 2)"); 

            builder.Property(l => l.FrozenCalculatedCost)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(l => l.DiscountPercentage)
                .IsRequired()
                .HasColumnType("decimal(5, 2)"); 

            builder.HasOne(l => l.ProductItem)
                .WithMany(p => p.DocumentLines)
                .HasForeignKey(l => l.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.DocumentHeader)
                .WithMany(d => d.DetailLines)
                .HasForeignKey(l => l.DocumentHeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.InventoryTransactions)
                .WithOne(it => it.DocumentDetailLine)
                .HasForeignKey(it => it.DocumentDetailLineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}