using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Features.Chat;

[Authorize]
public class ChatHub(IChatService chatService, ILogger<ChatHub> logger) : Hub
{
    private readonly IChatService _chatService = chatService;
    private readonly ILogger<ChatHub> _logger = logger;

    public async Task JoinChannel(int channelId)
    {
        var channel = await GetChannelOrThrow(channelId);

        await Groups.AddToGroupAsync(Context.ConnectionId, channel.Name);

        _logger.LogInformation("User joined {Channel} channel", channel.Name);
    }

    public async Task LeaveChannel(int channelId)
    {
        var channel = await GetChannelOrThrow(channelId);

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel.Name);

        _logger.LogInformation("User left {Channel} channel", channel.Name);
    }

    public async Task SendMessage(ChatMessageRequest request)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new HubException("User not found");

        var channel = await GetChannelOrThrow(request.ChannelId);
        var result = await _chatService.CreateMessageAsync(userId, request);

        if (result.IsFailure)
        {
            throw new HubException(result.Error.Message);
        }

        await Clients.Group(channel.Name).SendAsync("ReceiveMessage", result.Value);
    }

    private async Task<ChatChannel> GetChannelOrThrow(int channelId)
    {
        var result = await _chatService.GetChannelAsync(channelId);

        if (result.IsFailure || result.Value is null)
        {
            throw new HubException($"Channel with id {channelId} not found.");
        }

        return result.Value;
    }
}
