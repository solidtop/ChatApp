using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Features.Chat;

public class ChatHub(ILogger<ChatHub> logger) : Hub
{
    private readonly ILogger<ChatHub> _logger = logger;

    public async Task JoinChannel(int channelId)
    {

    }

    public async Task LeaveChannel(int channelId)
    {

    }

    public async Task SendMessage(ChatMessageRequest request)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return;


    }


    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
