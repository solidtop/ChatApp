
using System.Text.RegularExpressions;
using ChatApp.Server.Common.Results;
using ChatApp.Server.Data;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Features.Chat;

public class ChatService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ChatMessageValidator messageValidator) : IChatService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ChatMessageValidator _messageValidator = messageValidator;

    public async Task<List<ChatChannelResponse>> GetChannelsAsync(string userId)
    {
        var channels = await _context.ChatChannels.ToListAsync();
        var user = await _userManager.FindByIdAsync(userId);
        var userRoles = user != null ? await _userManager.GetRolesAsync(user) : [];

        return [.. channels.Select(channel => ChatChannelResponse.FromChatChannel(channel, userRoles))];
    }

    public async Task<Result<ChatChannel>> GetChannelAsync(int channelId)
    {
        var channel = await _context.ChatChannels.FindAsync(channelId);

        if (channel is null)
        {
            return ChatChannelErrors.NotFound(channelId);
        }

        return Result.Ok(channel);
    }

    public async Task<List<ChatMessageResponse>> GetLatestMessagesAsync(int channelId, int count)
    {
        var latestMessages = await _context.ChatMessages
            .Where(message => message.ChannelId == channelId)
            .OrderByDescending(message => message.Timestamp)
            .Take(count)
            .Reverse().ToListAsync();

        return [.. latestMessages.Select(ChatMessageResponse.FromChatMessage)];
    }

    public async Task<Result<ChatMessageResponse>> CreateMessageAsync(string userId, ChatMessageRequest request)
    {
        var validationResult = _messageValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<ChatMessageResponse>();
        }

        var channel = await _context.ChatChannels.FindAsync(request.ChannelId);

        if (channel is null)
        {
            return ChatChannelErrors.NotFound(request.ChannelId);
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var newMessage = new ChatMessage
        {
            Text = SanitizeText(request.Text),
            Timestamp = DateTime.UtcNow,
            Channel = channel,
            User = user,
        };

        await _context.ChatMessages.AddAsync(newMessage);
        await _context.SaveChangesAsync();

        return Result.Ok(ChatMessageResponse.FromChatMessage(newMessage));
    }

    private static string SanitizeText(string text)
    {
        string pattern = @"[^a-zA-Z0-9\s~`!@#$%^&*()-_=+{}|;:'<>,.?/\\[\]\""]";

        if (Regex.IsMatch(text, pattern))
        {
            text = Regex.Replace(text, pattern, "*");
        }

        return text;
    }
}

