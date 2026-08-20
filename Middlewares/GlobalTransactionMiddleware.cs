namespace ErpSystem.Middlewares;

public class GlobalTransactionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalTransactionMiddleware> _logger;

    public GlobalTransactionMiddleware(RequestDelegate next, ILogger<GlobalTransactionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        if (context.Request.Method == HttpMethods.Get)
        {
            await _next(context);
            return;
        }

        _logger.LogInformation("Starting transaction for {Method} {Path}", context.Request.Method, context.Request.Path);

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await _next(context);

            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                await unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Transaction committed successfully for {Method} {Path}", context.Request.Method, context.Request.Path);
            }
            else
            {
                await unitOfWork.RollbackTransactionAsync();
                _logger.LogWarning("Transaction rolled back due to {StatusCode} for {Method} {Path}", context.Response.StatusCode, context.Request.Method, context.Request.Path);
            }
        }
        catch
        {
            _logger.LogWarning("Rolling back transaction due to exception for {Method} {Path}", context.Request.Method, context.Request.Path);
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}