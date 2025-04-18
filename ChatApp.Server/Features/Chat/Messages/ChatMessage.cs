using ChatApp.Server.Features.Users;

namespace ChatApp.Server.Features.Chat.Messages;

public class ChatMessage
{
    public int Id { get; init; }
    public DateTime Timestamp { get; init; }
    public required UserSummary User { get; init; }
    public required string Text { get; init; }
    public string? TextColor { get; init; }
}

