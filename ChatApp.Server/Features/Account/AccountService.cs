
using ChatApp.Server.Features.Auth;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Account;

public class AccountService(UserManager<ApplicationUser> userManager) : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<AccountDetails?> GetAccountDetailsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);

        return AccountDetails.From(user, roles);
    }
}
