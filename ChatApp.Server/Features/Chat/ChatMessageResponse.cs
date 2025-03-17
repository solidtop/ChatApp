using ChatApp.Server.Features.Users;

namespace ChatApp.Server.Features.Chat;

public record ChatMessageResponse(int Id, string Text, string? TextColor, DateTime Timestamp, UserSummary User)
{
    public static ChatMessageResponse FromChatMessage(ChatMessage message)
    {
        return new(
            message.Id,
            message.Text,
            message.TextColor,
            message.Timestamp,
            UserSummary.FromUser(message.User)
        );
    }
}
