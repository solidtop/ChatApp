namespace ChatApp.Server.Features.Chat.Messages;

public interface IChannelMessageBuffer
{
    void Add(int channelId, ChatMessage message);
    IReadOnlyList<ChatMessage> GetAll(int channelId);
}
