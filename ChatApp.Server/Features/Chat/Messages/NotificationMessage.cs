namespace ChatApp.Server.Features.Chat.Messages;

public class NotificationMessage : ChatMessage
{
    public NotificationMessage()
    {
        Type = MessageType.Notification;
        TextColor = "#FFA500";
    }
}
