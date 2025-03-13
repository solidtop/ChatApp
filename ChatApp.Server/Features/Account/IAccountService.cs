using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Account.Requests;

namespace ChatApp.Server.Features.Account;

public interface IAccountService
{
    Task<Result> UpdateDisplayColorAsync(string userId, UpdateDisplayColorRequest request);
    Task<Result> UpdateAvatarAsync(string userId, UpdateAvatarRequest request);
}
