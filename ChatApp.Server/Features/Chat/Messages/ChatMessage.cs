using ChatApp.Server.Features.Users;

namespace ChatApp.Server.Features.Chat.Messages;

public class ChatMessage
{
    private static int _counter = 1;

    public int Id => _counter++;
    public MessageType Type { get; }
    public DateTime Timestamp => DateTime.UtcNow;
    public UserSummary? Sender { get; }
    public UserSummary? Receiver { get; }
    public string Content { get; }

    public ChatMessage(MessageType type, string content)
    {
        Type = type;
        Content = content;
    }

    public ChatMessage(MessageType type, UserSummary sender, string content)
    {
        Type = type;
        Sender = sender;
        Content = content;
    }

    public ChatMessage(MessageType type, UserSummary sender, UserSummary receiver, string content)
    {
        Type = type;
        Sender = sender;
        Receiver = receiver;
        Content = content;
    }
}

