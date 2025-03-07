using ChatApp.Server.Features.Auth;

namespace ChatApp.Server.Features.Account;

public record AccountDetails(
    string UserId,
    string? Username,
    string? Email,
    IList<string> Roles
    )
{
    public static AccountDetails FromUser(ApplicationUser user, IList<string> roles)
    {
        return new AccountDetails(
            user.Id,
            user.UserName,
            user.Email,
            roles
            );
    }
}
