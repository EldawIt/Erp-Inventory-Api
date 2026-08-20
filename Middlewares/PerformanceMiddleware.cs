using System.Diagnostics;

namespace ErpSystem.Middlewares;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;

    public PerformanceMiddleware(
        RequestDelegate next,
        ILogger<PerformanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            var elapsedMs = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation(
                "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsedMs);

            if (elapsedMs > 500)
            {
                _logger.LogWarning(
                    "Slow request: {Method} {Path} took {ElapsedMilliseconds}ms",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMs);
            }
        }
    }
}