using ChatApp.Server.Features.Auth;

namespace ChatApp.Server.Features.Chat;

public class ChatMessage
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public string? TextColor { get; set; }
    public DateTime Timestamp { get; set; }

    public int? ChannelId { get; set; }
    public ChatChannel? Channel { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}
