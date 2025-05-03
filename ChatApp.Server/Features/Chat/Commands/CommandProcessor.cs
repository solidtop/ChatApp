namespace ChatApp.Server.Features.Chat.Commands;

public class CommandProcessor(IEnumerable<IChatCommand> commands, IChatService chatService, ILogger<CommandProcessor> logger) : ICommandProcessor
{
    private readonly IEnumerable<IChatCommand> _commands = commands;
    private readonly IChatService _chatService = chatService;
    private readonly ILogger<CommandProcessor> _logger = logger;

    public async Task ProcessAsync(string userId, string commandText)
    {
        _logger.LogInformation("Processing command from user {UserId}: {CommandText}.", userId, commandText);

        if (!commandText.StartsWith('/'))
        {
            await NotifyErrorAsync(userId, "Commands must start with '/'");
            return;
        }

        var parts = commandText[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            await NotifyErrorAsync(userId, "You must specify a command name.");
            return;
        }

        var commandName = parts[0].ToLower();
        var args = parts.Skip(1).ToArray();

        var command = _commands.FirstOrDefault(cmd => cmd.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

        if (command is null)
        {
            await NotifyErrorAsync(userId, $"Unknown command '{commandName}.");
            return;
        }

        var result = await command.ExecuteAsync(userId, args);

        if (result.IsFailure)
        {
            _logger.LogWarning("Command '{CommandName}' execution failed: {Error}.", commandName, result.Error.Message);
            await NotifyErrorAsync(userId, result.Error.Message);
        }
        else
        {
            _logger.LogInformation("Successfully executed command '{CommandName}' with args: {Args}.", commandName, args);

            if (!string.IsNullOrEmpty(result.Value))
                await NotifySuccessAsync(userId, result.Value);
        }
    }

    private Task NotifySuccessAsync(string userId, string message) =>
        _chatService.SendNotificationAsync(userId, message);

    private Task NotifyErrorAsync(string userId, string error) =>
        _chatService.SendErrorAsync(userId, error);
}
