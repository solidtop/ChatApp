using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Chat.Messages;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Features.Chat;

public class ChatService(
    IHubContext<ChatHub> hub,
    IChannelService channelService,
    IChannelMessageBuffer messageBuffer,
    UserManager<ApplicationUser> userManager,
    ILogger<ChatService> logger
    ) : IChatService
{
    private readonly IHubContext<ChatHub> _hub = hub;
    private readonly IChannelService _channelService = channelService;
    private readonly IChannelMessageBuffer _messageBuffer = messageBuffer;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<ChatService> _logger = logger;

    private const int MinLength = 1;
    private const int MaxLength = 500;

    public async Task JoinChannelAsync(int channelId, string connectionId)
    {
        var channel = GetChannelOrThrow(channelId);
        await _hub.Groups.AddToGroupAsync(connectionId, channel.Name);
        _logger.LogInformation("User joined {Channel} channel.", channel.Name);
    }

    public async Task LeaveChannelAsync(int channelId, string connectionId)
    {
        var channel = GetChannelOrThrow(channelId);
        await _hub.Groups.RemoveFromGroupAsync(connectionId, channel.Name);
        _logger.LogInformation("User left {Channel} channel.", channel.Name);
    }

    public async Task SendChannelMessageAsync(string userId, int channelId, string content)
    {
        var user = await GetUserByIdOrThrow(userId);
        var channel = GetChannelOrThrow(channelId);
        ValidateContent(content);
        var message = new ChatMessage(MessageType.Channel, user, content);

        _messageBuffer.Add(channelId, message);

        await _hub.Clients
            .Group(channel.Name)
            .SendAsync("ReceiveMessage", message);
    }

    public async Task SendWhisperAsync(string senderId, string recipientName, string content)
    {
        var recipient = await GetUserByNameOrThrow(recipientName);
        ValidateContent(content);
        var whisper = new ChatMessage(MessageType.Whisper, recipient, content);

        await _hub.Clients.User(senderId).SendAsync("ReceiveMessage", whisper);
        await _hub.Clients.User(recipient.Id).SendAsync("ReceiveMessage", whisper);
    }

    public Task SendNotificationAsync(string userId, string content)
    {
        ValidateContent(content);
        var message = new ChatMessage(MessageType.Notification, content);

        return _hub.Clients
            .User(userId)
            .SendAsync("ReceiveMessage", message);
    }

    public Task SendErrorAsync(string userId, string content)
    {
        ValidateContent(content);
        var message = new ChatMessage(MessageType.Error, content);

        return _hub.Clients
            .User(userId)
            .SendAsync("ReceiveMessage", message);
    }

    public Task SendAnnouncementAsync(string content)
    {
        ValidateContent(content);
        var message = new ChatMessage(MessageType.Announcement, content);
        return _hub.Clients.All.SendAsync("ReceiveMessage", message);
    }

    private ChatChannel GetChannelOrThrow(int channelId)
    {
        var result = _channelService.GetChannelById(channelId);

        if (result.IsFailure || result.Value is null)
        {
            throw new HubException($"Channel with id {channelId} not found.");
        }

        return result.Value;
    }

    private async Task<UserSummary> GetUserByIdOrThrow(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId!) ?? throw new HubException("User not found.");
        return UserSummary.FromUser(user);
    }

    private async Task<UserSummary> GetUserByNameOrThrow(string name)
    {
        var user = await _userManager.FindByNameAsync(name) ?? throw new HubException("User not found.");
        return UserSummary.FromUser(user);
    }

    private static void ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content) ||
            content.Length < MinLength ||
            content.Length > MaxLength)
        {
            throw new HubException("Invalid message content");
        }
    }
}
