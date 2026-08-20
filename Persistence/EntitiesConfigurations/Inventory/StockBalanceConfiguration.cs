namespace ErpSystem.Persistence.EntitiesConfigurations.Inventorey
{
    public class StockBalanceConfiguration : IEntityTypeConfiguration<StockBalance>
    {
        public void Configure(EntityTypeBuilder<StockBalance> builder)
        {
            builder.ToTable("StockBalances");

            builder.HasKey(sb => sb.Id);

            builder.HasIndex(sb => new { sb.WarehouseId, sb.ProductItemId })
                .IsUnique();

            builder.Property(sb => sb.RealQuantityOnShelf)
                .IsRequired()
                .HasColumnType("decimal(18,3)");

            builder.Property(sb => sb.SafetyReorderLevel)
                .IsRequired()
                .HasColumnType("decimal(18,3)");


            builder.Property(sb => sb.CurrentAverageCost)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(sb => sb.DatabaseVersion)
                .IsRowVersion();
 

            builder.Property(sb => sb.TotalValue)
                 .IsRequired()
                  .HasColumnType("decimal(18,2)");

            builder.HasOne(sb => sb.Warehouse)
                .WithMany(w => w.StockBalances)
                .HasForeignKey(sb => sb.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sb => sb.ProductItem)
                .WithMany(p => p.StockBalances)  
                .HasForeignKey(sb => sb.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}