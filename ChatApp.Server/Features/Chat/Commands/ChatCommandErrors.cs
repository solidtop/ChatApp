using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public class ChatCommandErrors
{
    public static Error Invalid(string message) =>
        new(ErrorType.Validation, message);
}
