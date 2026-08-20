


using ErpSystem.Services.Inventory.Interfaces;

namespace ErpSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDatabaseConfig(configuration);

            services.AddSwaggerConfig();

            services.AddServices();

            services.AddValidators();
            services.AddMapsterConfig();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddHostedService<DatabaseWarmupService>();
            services.AddHttpContextAccessor();

            return services;
        }

        private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }

        private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IStockBalanceService, StockBalanceService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentPostingService, DocumentPostingService>();
            services.AddSingleton<ICostCalculator, CostCalculator>(); 
             
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services
       .AddFluentValidationAutoValidation()
       .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}