using ChatApp.Server.Common.Results;
using ChatApp.Server.Data;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Account.Requests;
using ChatApp.Server.Features.Account.Validators;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Avatars;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Account;

public class AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ColorValidator colorValidator) : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;
    private readonly ColorValidator _colorValidator = colorValidator;

    public async Task<Result> UpdateDisplayColorAsync(string userId, UpdateDisplayColorRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return Result.Fail(UserErrors.NotFound(userId));
        }

        var color = request.Color;
        var validationResult = _colorValidator.Validate(color);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult();
        }

        user.DisplayColor = color;

        var updateResult = await _userManager.UpdateAsync(user);
        return updateResult.ToResult();
    }

    public async Task<Result> UpdateAvatarAsync(string userId, UpdateAvatarRequest request)
    {
        var avatarId = request.AvatarId;
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
