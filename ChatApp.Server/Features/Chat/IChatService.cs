using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat;

public interface IChatService
{
    Task<List<ChatChannel>> GetChannelsAsync();
    Task<Result<ChatChannel>> GetChannelAsync(int channelId);
    Task<List<ChatMessageResponse>> GetLatestMessagesAsync(int channelId, int count);
    Task<Result<ChatMessageResponse>> CreateMessageAsync(string userId, ChatMessageRequest request);
}
