using ChatApp.Server.Features.Avatars;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Auth;

public class ApplicationUser : IdentityUser
{
    public int? AvatarId { get; set; }
    public Avatar? Avatar { get; set; }
}
