using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public class CommandProcessor(IEnumerable<IChatCommand> commands, ILogger<CommandProcessor> logger) : ICommandProcessor
{
    private readonly IEnumerable<IChatCommand> _commands = commands;
    private readonly ILogger<CommandProcessor> _logger = logger;

    public async Task<Result<string>> ProcessAsync(string userId, string commandText)
    {
        _logger.LogInformation("Processing command from user {UserId}: {CommandText}.", userId, commandText);

        if (!commandText.StartsWith('/'))
        {
            _logger.LogWarning("Invalid command format from user {UserId}: {CommandText}.", userId, commandText);
            return Result.Fail<string>("Invalid command format.");
        }

        var parts = commandText[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            _logger.LogWarning("No command specified from user {UserId}.", userId);
            return Result.Fail<string>("No command specified.");
        }

        var commandName = parts[0].ToLower();
        var args = parts.Skip(1).ToArray();

        var command = _commands.FirstOrDefault(cmd => cmd.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

        if (command is null)
        {
            _logger.LogWarning("Command '/{CommandName}' not recognized from user {UserId}.", commandName, userId);
            return Result.Fail<string>($"Command '/{commandName}' not recognized.");
        }

        var result = await command.ExecuteAsync(userId, args);

        if (result.IsFailure)
        {
            _logger.LogWarning("Command '{CommandName}' execution failed: {Error}.", commandName, result.Error.Message);
            return result;
        }

        _logger.LogInformation("Successfully executed command '{CommandName}' with args: {Args}.", commandName, args);

        return result;
    }
}
