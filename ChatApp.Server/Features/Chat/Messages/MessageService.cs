using ChatApp.Server.Common.Results;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Chat.Messages;

public class MessageService(
    UserManager<ApplicationUser> userManager,
    IChannelService channelService,
    IMessageBuffer buffer,
    MessageValidator validator
    ) : IMessageService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IChannelService _channelService = channelService;
    private readonly IMessageBuffer _buffer = buffer;
    private readonly MessageValidator _validator = validator;

    private static int _id = 1;

    public Result<IReadOnlyList<ChatMessage>> GetLatestMessages(int channelId)
    {
        var result = _channelService.GetChannelById(channelId);

        if (result.IsFailure)
        {
            return ChannelErrors.NotFound(channelId);
        }

        return Result.Ok(_buffer.GetAll(channelId));
    }

    public async Task<Result<ChatMessage>> CreateMessageAsync(string userId, ChatMessageRequest request)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<ChatMessage>();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var newMessage = new ChatMessage
        {
            Id = _id++,
            Timestamp = DateTime.UtcNow,
            User = UserSummary.FromUser(user),
            Text = request.Text,
        };

        _buffer.Add(request.ChannelId, newMessage);

        return Result.Ok(newMessage);
    }
}

