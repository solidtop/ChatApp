using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public interface IChatCommand
{
    string Command { get; }
    Task<Result> ExecuteAsync(string userId, string[] args);
}
