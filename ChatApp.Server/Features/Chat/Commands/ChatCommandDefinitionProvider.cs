namespace ChatApp.Server.Features.Chat.Commands;

public class ChatCommandDefinitionProvider(IEnumerable<IChatCommand> commands)
{
    private readonly IEnumerable<IChatCommand> _commands = commands;

    public List<ChatCommandDefinition> GetDefinitions()
    {
        return [.. _commands.Select(ChatCommandDefinition.FromChatCommand)];
    }
}
