namespace ChatApp.Server.Features.Chat.Messages;

public abstract class ChatMessage
{
    public int Id { get; set; }
    public MessageType Type { get; set; }
    public DateTime Timestamp { get; set; }
    public required string Text { get; set; }
    public string? TextColor { get; set; }
}
