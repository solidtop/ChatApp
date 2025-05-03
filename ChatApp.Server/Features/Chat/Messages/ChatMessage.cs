using ChatApp.Server.Features.Users;

namespace ChatApp.Server.Features.Chat.Messages;

public class ChatMessage
{
    private static int _counter = 1;

    public int Id => _counter++;
    public MessageType Type { get; }
    public DateTime Timestamp => DateTime.UtcNow;
    public UserSummary? User { get; }
    public string Content { get; }

    public ChatMessage(MessageType type, string content)
    {
        Type = type;
        Content = content;
    }

    public ChatMessage(MessageType type, UserSummary? user, string content)
    {
        Type = type;
        User = user;
        Content = content;
    }
}

