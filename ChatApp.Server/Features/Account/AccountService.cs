using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Account;

public class AccountService(UserManager<ApplicationUser> userManager) : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

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
}
