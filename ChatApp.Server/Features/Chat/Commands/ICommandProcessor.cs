namespace ChatApp.Server.Features.Chat.Commands;

public interface ICommandProcessor
{
    Task ProcessAsync(string userId, string commandText);
}
