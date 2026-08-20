 

public class DatabaseWarmupService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseWarmupService> _logger;

    public DatabaseWarmupService(IServiceProvider serviceProvider, ILogger<DatabaseWarmupService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting EF Core DbContext warm-up...");

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        _ = dbContext.Model;

        await dbContext.Database.ExecuteSqlRawAsync("SELECT 1;", cancellationToken);

        _logger.LogInformation("EF Core DbContext warm-up completed successfully.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}