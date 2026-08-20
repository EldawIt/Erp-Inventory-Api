



namespace ErpSystem.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<StockBalance> StockBalances { get; set; }
        public DbSet<DocumentHeader> DocumentHeaders { get; set; }
        public DbSet<DocumentDetailLine> DocumentDetailLines { get; set; }
        public DbSet<FifoBatchQueue> FifoBatchQueues { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
    }
}