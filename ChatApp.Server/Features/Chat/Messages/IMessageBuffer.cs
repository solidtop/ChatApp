namespace ChatApp.Server.Features.Chat.Messages;

public interface IMessageBuffer
{
    void Add(int channelId, ChatMessage message);
    IReadOnlyList<ChatMessage> GetAll(int channelId);
}
