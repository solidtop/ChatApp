namespace ChatApp.Server.Features.Chat.Commands;

public record CommandDefinition(string Name, string Description, string Usage)
{
    public static CommandDefinition FromCommand(IChatCommand command)
    {
        return new(command.Name, command.Description, command.Usage);
    }
}
