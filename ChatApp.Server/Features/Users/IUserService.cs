﻿using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Users;

public interface IUserService
{
    Task<List<UserSummary>> GetUserSummaries();
    Task<Result<UserProfile>> GetUserProfile(string userId);
}
