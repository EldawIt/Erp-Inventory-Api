namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey
{
    public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("ProductItems");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.InternalItemCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.NameArabic)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.NameEnglish)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.ItemType)
                .IsRequired();

            builder.Property(p => p.SellingPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
           .HasDefaultValue(true);

            builder.Property(p => p.TaxRate)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0.14m);

            builder.Property(p => p.CalculationMethod)
                .IsRequired();

            builder.HasIndex(p => p.InternalItemCode)
                .IsUnique(); 

            builder.HasIndex(p => p.NameArabic);
            builder.HasIndex(p => p.NameEnglish);

            builder.HasIndex(p => new { p.IsDeleted, p.Id });

            builder.HasMany(p => p.StockBalances)
                .WithOne(sb => sb.ProductItem)
                .HasForeignKey(sb => sb.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.DocumentLines)
                .WithOne(d => d.ProductItem)
                .HasForeignKey(d => d.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.FifoBatches)
                .WithOne(f => f.ProductItem)
                .HasForeignKey(f => f.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.InventoryTransactions)
                .WithOne(it => it.ProductItem)
                .HasForeignKey(it => it.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}