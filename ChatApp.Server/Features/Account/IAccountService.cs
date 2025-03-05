namespace ChatApp.Server.Features.Account;

public interface IAccountService
{
    Task<AccountDetails?> GetAccountDetailsAsync(string userId);
}
