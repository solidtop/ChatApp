namespace ChatApp.Server.Features.Chat.Messages;

public interface IChannelMessageBuffer
{
    void Add(int channelId, ChannelMessage message);
    IReadOnlyList<ChannelMessage> GetAll(int channelId);
}
