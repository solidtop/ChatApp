using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Common.Results;

public static class ApiResults
{
    public static ActionResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var error = result.Error;

        var problemDetails = new ProblemDetails
        {
            Type = GetType(error.Type),
            Title = GetTitle(error.Type),
            Detail = error.Message,
            Status = GetStatusCode(error.Type),
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                _ => "https://tools.ietf.org/html/rfc9110#section-15.6.1"
            };

        static string GetTitle(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.NotFound => "Not Found",
                _ => "Internal Server Error"
            };

        static int GetStatusCode(ErrorType errorType) =>
           errorType switch
           {
               ErrorType.NotFound => StatusCodes.Status404NotFound,
               _ => StatusCodes.Status500InternalServerError
           };
    }
}
