using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Chat.Commands;
using ChatApp.Server.Features.Chat.Messages;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Features.Chat;

public class ChatHub(
    IChannelService channelService,
    IMessageService messageService,
    ICommandProcessor commandProcessor,
    ILogger<ChatHub> logger
    ) : Hub
{
    private readonly IChannelService _channelService = channelService;
    private readonly IMessageService _messageService = messageService;
    private readonly ICommandProcessor _commandProcessor = commandProcessor;
    private readonly ILogger<ChatHub> _logger = logger;

    public async Task JoinChannel(int channelId)
    {
        var channel = GetChannelOrThrow(channelId);
        await Groups.AddToGroupAsync(Context.ConnectionId, channel.Name);
        _logger.LogInformation("User joined {Channel} channel", channel.Name);
    }

    public async Task LeaveChannel(int channelId)
    {
        var channel = GetChannelOrThrow(channelId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel.Name);
        _logger.LogInformation("User left {Channel} channel", channel.Name);
    }

    public async Task SendMessage(ChatMessageRequest request)
    {
        var channel = GetChannelOrThrow(request.ChannelId);
        var userId = GetUserIdOrThrow();
        var result = await _messageService.CreateMessageAsync(userId, request);

        if (result.IsFailure)
        {
            throw new HubException(result.Error.Message);
        }

        await Clients.Group(channel.Name).SendAsync("ReceiveMessage", result.Value);
    }

    public async Task ExecuteCommand(string commandText)
    {
        var userId = GetUserIdOrThrow();
        await _commandProcessor.ProcessAsync(userId, commandText);
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

    private string GetUserIdOrThrow()
    {
        return Context.UserIdentifier ?? throw new HubException("User not found");
    }
}
