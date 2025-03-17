
using ChatApp.Server.Common.Results;
using ChatApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Features.Chat;

public class ChatService(ApplicationDbContext context) : IChatService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<ChatChannel>> GetChannelsAsync()
    {
        return await _context.ChatChannels.ToListAsync();
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
}
