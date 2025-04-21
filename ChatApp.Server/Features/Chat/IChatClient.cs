using ChatApp.Server.Features.Chat.Messages;

namespace ChatApp.Server.Features.Chat;

public interface IChatClient
{
    Task ReceiveMessage(ChatMessage message);
}
