namespace ChatApp.Server.Features.Chat.Commands;

public record ChatCommandDefinition(string Name, string Description, string Usage)
{
    public static ChatCommandDefinition FromChatCommand(IChatCommand command)
    {
        return new(command.Name, command.Description, command.Usage);
    }
}
