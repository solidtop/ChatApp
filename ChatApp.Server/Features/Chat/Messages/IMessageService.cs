using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Messages;

public interface IMessageService
{
    Result<IReadOnlyList<ChannelMessage>> GetLatestMessages(int channelId);
    Task<Result<ChannelMessage>> CreateMessageAsync(string userId, ChatMessageRequest request);
    NotificationMessage CreateNotification(string text);
}
