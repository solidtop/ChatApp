using ChatApp.Server.Common.Results;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Features.Chat.Messages;

public class MessageService(
    IHubContext<ChatHub> hubContext,
    UserManager<ApplicationUser> userManager,
    IChannelService channelService,
    IChannelMessageBuffer buffer,
    MessageValidator validator
    ) : IMessageService
{
    private readonly IHubContext<ChatHub> _hubContext = hubContext;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IChannelService _channelService = channelService;
    private readonly IChannelMessageBuffer _buffer = buffer;
    private readonly MessageValidator _validator = validator;

    private static int _id = 1;

    public Result<IReadOnlyList<ChannelMessage>> GetLatestMessages(int channelId)
    {
        var result = _channelService.GetChannelById(channelId);

        if (result.IsFailure)
        {
            return ChannelErrors.NotFound(channelId);
        }

        return Result.Ok(_buffer.GetAll(channelId));
    }

    public async Task<Result<ChannelMessage>> CreateMessageAsync(string userId, ChatMessageRequest request)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<ChannelMessage>();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var newMessage = new ChannelMessage
        {
            Id = _id++,
            Timestamp = DateTime.UtcNow,
            User = UserSummary.FromUser(user),
            Text = request.Text,
        };

        _buffer.Add(request.ChannelId, newMessage);

        return Result.Ok(newMessage);
    }

    public NotificationMessage CreateNotification(string text)
    {
        return new NotificationMessage
        {
            Id = _id++,
            Timestamp = DateTime.UtcNow,
            Text = text,
        };
    }
}

