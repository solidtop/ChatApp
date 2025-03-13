using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Features.Users;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public Task<List<UserSummary>> GetUserSummaries()
    {
        return _userManager.Users.Select(user => UserSummary.FromUser(user)).ToListAsync();
    }

    public async Task<Result<UserProfile>> GetUserProfile(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return UserProfile.FromUser(user, roles);
    }
}
