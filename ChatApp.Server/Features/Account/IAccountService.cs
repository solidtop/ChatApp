using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Account;

public interface IAccountService
{
    Task<Result<AccountDetails>> GetAccountDetailsAsync(string userId);
    Task<Result> UpdateDisplayColorAsync(string userId, string color);
    Task<Result> UpdateAvatarAsync(string userId, int avatarId);
}
