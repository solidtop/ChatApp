using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Avatars;

namespace ChatApp.Server.Features.Users;

public record UserProfile(
    string Id,
    string? Username,
    IList<string> Roles,
    string? DisplayColor,
    Avatar? Avatar
    )
{
    public static UserProfile FromUser(ApplicationUser user, IList<string> roles)
    {
        return new UserProfile(
            user.Id,
            user.UserName,
            roles,
            user.DisplayColor,
            user.Avatar
            );
    }
}
