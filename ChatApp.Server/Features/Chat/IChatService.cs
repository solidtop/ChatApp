namespace ChatApp.Server.Features.Chat;

public interface IChatService
{
    Task JoinChannelAsync(int channelId, string connectionId);
    Task LeaveChannelAsync(int channelId, string connectionId);
    Task SendChannelMessageAsync(string userId, int channelId, string content);
    Task SendWhisperAsync(string senderId, string recipientName, string content);
    Task SendNotificationAsync(string userId, string content);
    Task SendErrorAsync(string userId, string content);
    Task SendAnnouncementAsync(string content);
}
