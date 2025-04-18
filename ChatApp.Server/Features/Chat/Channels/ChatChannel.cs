namespace ChatApp.Server.Features.Chat.Channels;

public abstract class ChatChannel
{
    public abstract int Id { get; }
    public abstract string Name { get; }
    public abstract string Description { get; }
}

