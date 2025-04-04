using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public class ChatCommandProcessor(IEnumerable<IChatCommand> commands, ILogger<ChatCommandProcessor> logger) : IChatCommandProcessor
{
    private readonly IEnumerable<IChatCommand> _commands = commands;
    private readonly ILogger<ChatCommandProcessor> _logger = logger;

    public async Task<Result> ProcessAsync(string userId, string commandText)
    {
        _logger.LogInformation("Processing command from user {UserId}: {CommandText}", userId, commandText);

        if (!commandText.StartsWith('/'))
        {
            _logger.LogWarning("Invalid command format from user {UserId}: {CommandText}", userId, commandText);
            return Result.Fail("Invalid command format");
        }

        var parts = commandText[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
        {
            _logger.LogWarning("No command specified from user {UserId}", userId);
            return Result.Fail("No command specified");
        }

        var commandName = parts[0].ToLower();
        var args = parts.Skip(1).ToArray();

        var command = _commands.FirstOrDefault(c => c.Command.Equals(commandName, StringComparison.OrdinalIgnoreCase));

        if (command is null)
        {
            _logger.LogWarning("Command '/{CommandName}' not recognized from user {UserId}", commandName, userId);
            return Result.Fail($"Command '/{commandName}' not recognized");
        }

        var result = await command.ExecuteAsync(userId, args);

        if (result.IsFailure)
        {
            _logger.LogWarning("Command '{CommandName}' execution failed: {Error}", commandName, result.Error.Message);
        }

        _logger.LogInformation("Successfully executed command '{CommandName}' with args: {Args}", commandName, args);

        return result;
    }
}
