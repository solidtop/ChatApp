using ChatApp.Server.Common.Results;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return Result.Ok();
        }

        var errorMessage = string.Join("; ", identityResult.Errors.Select(e => e.Description));
        return Result.Fail(new Error(ErrorType.Validation, errorMessage));
    }

    public static Result<T> ToResult<T>(this IdentityResult identityResult, T value)
    {
        return identityResult.Succeeded
            ? Result.Ok(value)
            : Result.Fail<T>(new Error(ErrorType.Validation, string.Join("; ", identityResult.Errors.Select(e => e.Description))));
    }
}
