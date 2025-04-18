using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Channels;

public static class ChannelErrors
{
    public static Error NotFound(int channelId) =>
        new(ErrorType.NotFound, ErrorMessages.NotFound("Channel", channelId.ToString()));
}
