using ChatApp.Server.Common.Results;
using FluentValidation.Results;

namespace ChatApp.Server.Extensions;

public static class ValidationResultExtensions
{
    public static Result ToResult(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        return Result.Fail(new ValidationError("One or more errors occurred", errors));
    }
}
