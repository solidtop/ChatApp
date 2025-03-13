using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Users;

public interface IUserService
{
    Task<List<UserSummary>> GetUserSummariesAsync();
    Task<Result<UserProfile>> GetUserProfileAsync(string userId);
}
