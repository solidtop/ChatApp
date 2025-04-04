using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public interface IChatCommandProcessor
{
    Task<Result> ProcessAsync(string userId, string commandText);
}
