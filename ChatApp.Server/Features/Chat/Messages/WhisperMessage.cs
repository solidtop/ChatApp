using ChatApp.Server.Features.Users;

namespace ChatApp.Server.Features.Chat.Messages;

public class WhisperMessage : ChatMessage
{
    public required UserSummary User { get; set; }

    public WhisperMessage()
    {
        Type = MessageType.Whisper;
    }
}
