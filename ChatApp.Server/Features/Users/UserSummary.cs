using ChatApp.Server.Features.Auth;

namespace ChatApp.Server.Features.Users;

public record UserSummary(
    string Id,
    string? Username
    )
{
    public static UserSummary FromUser(ApplicationUser user)
    {
        return new UserSummary(
            user.Id,
            user.UserName
            );
    }
}
