using ChatApp.Server.Features.Chat.Commands;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Features.Chat;

public class ChatHub(IChatService chatService, ICommandProcessor commandProcessor) : Hub<IChatClient>
{
    private readonly IChatService _chatService = chatService;
    private readonly ICommandProcessor _commandProcessor = commandProcessor;

    public Task JoinChannel(int channelId) =>
        _chatService.JoinChannelAsync(channelId, Context.ConnectionId);

    public Task LeaveChannel(int channelId) =>
        _chatService.LeaveChannelAsync(channelId, Context.ConnectionId);

    public Task SendChannelMessage(int channelId, string content) =>
        _chatService.SendChannelMessageAsync(Context.UserIdentifier!, channelId, content);

    public Task ExecuteCommand(string commandText) =>
        _commandProcessor.ProcessAsync(Context.UserIdentifier!, commandText);
}
