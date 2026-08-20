
namespace ErpSystem.Abstractions;

public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert success result to problem");

        var statusCode = result.Error.StatusCode ?? 500;
        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = result.Error.Code,
            Detail = result.Error.Description,
            Extensions = new Dictionary<string, object?>
            {
                ["errors"] = new[] { result.Error.Description }
            }
        };

        return new ObjectResult(problem)
        {
            StatusCode = statusCode
        };
    }

    public static ObjectResult ToProblem<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert success result to problem");

        var statusCode = result.Error.StatusCode ?? 500;
        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = result.Error.Code,
            Detail = result.Error.Description,
            Extensions = new Dictionary<string, object?>
            {
                ["errors"] = new[] { result.Error.Description }
            }
        };

        return new ObjectResult(problem)
        {
            StatusCode = statusCode
        };
    }
}