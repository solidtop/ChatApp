using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Chat.Messages;

namespace ChatApp.Server.Features.Chat.Commands;

public class WhisperCommand(IMessageService messageService) : IChatCommand
{
    private readonly IMessageService _messageService = messageService;

    public string Name => "w";

    public string Description => "Whisper to your friends";

    public string Usage => "/w <user_name> <message>";

    public async Task<Result<string>> ExecuteAsync(string userId, string[] args)
    {
        var targetName = args.FirstOrDefault();
        var text = string.Join(' ', args.Skip(1));

        if (targetName is null || string.IsNullOrEmpty(text))
        {
            return Result.Fail<string>(Usage);
        }

        var result = await _messageService.SendWhisperAsync(userId, targetName, text);

        if (result.IsFailure)
        {
            return Result.Fail<string>(result.Error.Message);
        }

        return Result.Ok("");
    }
}
