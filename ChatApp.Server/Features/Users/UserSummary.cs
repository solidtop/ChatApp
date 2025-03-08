using ChatApp.Server.Features.Auth;

namespace ChatApp.Server.Features.Users;

public record UserSummary(
    string Id,
    string? Username,
    string? DisplayColor,
    string? AvatarImageUrl
    )
{
    public static UserSummary FromUser(ApplicationUser user)
    {
        return new UserSummary(
            user.Id,
            user.UserName,
            user.DisplayColor,
            user.Avatar?.ImageUrl
            );
    }
}
