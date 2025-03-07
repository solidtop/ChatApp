using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Common.Exceptions;

public class ApiExceptionHandler(IProblemDetailsService problemService) : IExceptionHandler
{
    private readonly IProblemDetailsService problemService = problemService;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            _ => StatusCodes.Status500InternalServerError
        };

        var activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;

        return await problemService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occured",
                Detail = exception.Message,
                Status = httpContext.Response.StatusCode,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                Extensions = new Dictionary<string, object?>
                {
                    { "RequestId", httpContext.TraceIdentifier },
                    { "TraceId", activity?.Id  },
                }
            }
        });
    }
}
