using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Chat.Commands;

public class WhisperCommand(IChatService chatService) : IChatCommand
{
    private readonly IChatService _chatService = chatService;

    public string Name => "w";

    public string Description => "Whisper to your friends";

    public string Usage => "/w <username> <message>";

    public async Task<Result<string>> ExecuteAsync(string userId, string[] args)
    {
        var recipientName = args.FirstOrDefault();
        var content = string.Join(' ', args.Skip(1));

        if (recipientName is null || string.IsNullOrEmpty(content))
        {
            return Result.Fail<string>(Usage);
        }

        await _chatService.SendWhisperAsync(userId, recipientName, content);

        return Result.Ok("");
    }
}
