using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Users;

public static class UserErrors
{
    public static Error NotFound(string userId) => new(
        ErrorType.NotFound, $"User with id {userId} not found");
}
