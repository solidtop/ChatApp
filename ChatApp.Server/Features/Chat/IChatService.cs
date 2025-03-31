using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Chat.Channels;

namespace ChatApp.Server.Features.Chat;

public interface IChatService
{
    Task<List<ChatChannelResponse>> GetChannelsAsync(string userId);
    Task<Result<ChatChannel>> GetChannelAsync(int channelId);
    Task<List<ChatMessageResponse>> GetLatestMessagesAsync(int channelId, int count);
    Task<Result<ChatMessageResponse>> CreateMessageAsync(string userId, ChatMessageRequest request);
}
