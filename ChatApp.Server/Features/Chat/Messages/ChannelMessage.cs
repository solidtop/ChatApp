using ChatApp.Server.Features.Users;

namespace ChatApp.Server.Features.Chat.Messages;

public class ChannelMessage : ChatMessage
{
    public required UserSummary User { get; init; }

    public ChannelMessage()
    {
        Type = MessageType.Channel;
    }
}
