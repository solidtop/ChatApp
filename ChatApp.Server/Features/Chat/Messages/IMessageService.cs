using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Messages;

public interface IMessageService
{
    Result<IReadOnlyList<ChatMessage>> GetLatestMessages(int channelId);
    Task<Result<ChatMessage>> CreateMessageAsync(string userId, ChatMessageRequest request);
}
