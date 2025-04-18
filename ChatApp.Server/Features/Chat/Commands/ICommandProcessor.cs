using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public interface ICommandProcessor
{
    Task<Result> ProcessAsync(string userId, string commandText);
}
