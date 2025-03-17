﻿using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat;

public class ChatMessageErrors
{
    public static Error NotFound(int channelId) =>
        new(ErrorType.NotFound, ErrorMessages.NotFound("ChatChannel", channelId.ToString()));
}
