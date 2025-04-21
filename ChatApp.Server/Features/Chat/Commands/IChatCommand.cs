using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public interface IChatCommand
{
    string Name { get; }
    string Description { get; }
    string Usage { get; }

    Task<Result<string>> ExecuteAsync(string userId, string[] args);
}
