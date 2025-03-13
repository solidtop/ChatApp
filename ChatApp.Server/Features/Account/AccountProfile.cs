using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Avatars;

namespace ChatApp.Server.Features.Account;

public record AccountProfile(
    string UserId,
    string? Username,
    string? Email,
    IList<string> Roles,
    string? DisplayColor,
    Avatar? Avatar
    )
{
    public static AccountProfile FromUser(ApplicationUser user, IList<string> roles)
    {
        return new AccountProfile(
            user.Id,
            user.UserName,
            user.Email,
            roles,
            user.DisplayColor,
            user.Avatar
            );
    }
}