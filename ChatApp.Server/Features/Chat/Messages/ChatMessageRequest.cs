namespace ChatApp.Server.Features.Chat.Messages;

public record ChatMessageRequest(
    int ChannelId,
    string Text
);
