using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Account;

public interface IAccountService
{
    Task<Result<AccountDetails>> GetAccountDetailsAsync(string userId);
}
