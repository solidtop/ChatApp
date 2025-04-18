using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Channels;

public class ChannelService(IEnumerable<ChatChannel> channels) : IChannelService
{
    private readonly List<ChatChannel> _channels = [.. channels];

    public IReadOnlyList<ChatChannel> GetAllChannels() => _channels;

    public Result<ChatChannel> GetChannelById(int id)
    {
        var channel = _channels.FirstOrDefault(channel => channel.Id == id);

        if (channel is null)
        {
            return ChannelErrors.NotFound(id);
        }

        return channel;
    }
}
