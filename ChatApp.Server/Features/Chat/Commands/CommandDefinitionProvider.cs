namespace ChatApp.Server.Features.Chat.Commands;

public class CommandDefinitionProvider(IEnumerable<IChatCommand> commands)
{
    private readonly IEnumerable<IChatCommand> _commands = commands;

    public List<CommandDefinition> GetDefinitions()
    {
        return [.. _commands.Select(CommandDefinition.FromCommand)];
    }
}