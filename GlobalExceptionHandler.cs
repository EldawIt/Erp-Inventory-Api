namespace ErpSystem;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Something went wrong: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };

        if (exception is DbUpdateConcurrencyException)
        {
            problemDetails.Status = StatusCodes.Status409Conflict;
            problemDetails.Title = "Conflict";
            problemDetails.Detail = "The data was modified by another user. Please refresh and try again.";
            _logger.LogWarning(exception, "Concurrency conflict");
        }
        else if (exception is ValidationException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Validation Error";
            problemDetails.Detail = "One or more validation errors occurred.";
        }
        else if (exception is ArgumentException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Bad Request";
            problemDetails.Detail = exception.Message; 
        }
        else if (exception is InvalidOperationException)
        {
            problemDetails.Status = StatusCodes.Status409Conflict;
            problemDetails.Title = "Conflict";
            problemDetails.Detail = exception.Message; 
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }
}