using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Channels;

public interface IChannelService
{
    IReadOnlyList<ChatChannel> GetAllChannels();
    Result<ChatChannel> GetChannelById(int id);
}
