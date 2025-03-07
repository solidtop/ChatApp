using ChatApp.Server.Features.Auth;

namespace ChatApp.Server.Features.Users;

public record UserDetails(
    string Id,
    string? Username,
    IList<string> Roles
    )
{
    public static UserDetails FromUser(ApplicationUser user, IList<string> roles)
    {
        return new UserDetails(user.Id, user.UserName, roles);
    }
}
