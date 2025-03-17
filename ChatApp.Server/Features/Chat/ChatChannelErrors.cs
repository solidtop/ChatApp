using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat;

public static class ChatChannelErrors
{
    public static Error NotFound(int channelId) =>
        new(ErrorType.NotFound, ErrorMessages.NotFound("Channel", channelId.ToString()));
}
