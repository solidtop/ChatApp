using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Avatars;

namespace ChatApp.Server.Features.Users;

public record UserDetails(
    string Id,
    string? Username,
    IList<string> Roles,
    Avatar? Avatar
    )
{
    public static UserDetails FromUser(ApplicationUser user, IList<string> roles)
    {
        return new UserDetails(
            user.Id,
            user.UserName,
            roles,
            user.Avatar
            );
    }
}
