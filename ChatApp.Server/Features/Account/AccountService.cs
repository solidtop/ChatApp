using ChatApp.Server.Common.Results;
using ChatApp.Server.Data;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Avatars;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Account;

public class AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<AccountDetails>> GetAccountDetailsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return AccountDetails.FromUser(user, roles);
    }

    public async Task<Result> UpdateDisplayColorAsync(string userId, string color)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Fail(UserErrors.NotFound(userId));
        }

        user.DisplayColor = color;

        var updateResult = await _userManager.UpdateAsync(user);
        return updateResult.ToResult();
    }

    public async Task<Result> UpdateAvatarAsync(string userId, int avatarId)
    {
        var avatar = await _context.Avatars.FindAsync(avatarId);

        if (avatar is null)
        {
            return Result.Fail(AvatarErrors.NotFound(avatarId));
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Fail(UserErrors.NotFound(userId));
        }

        user.AvatarId = avatarId;

        var updateResult = await _userManager.UpdateAsync(user);
        return updateResult.ToResult();
    }
}
