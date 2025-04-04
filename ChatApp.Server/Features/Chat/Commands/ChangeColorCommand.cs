
using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Account;
using ChatApp.Server.Features.Account.Requests;

namespace ChatApp.Server.Features.Chat.Commands;

public class ChangeColorCommand(IAccountService accountService) : IChatCommand
{
    private readonly IAccountService _accountService = accountService;

    public string Command => "color";

    public async Task<Result> ExecuteAsync(string userId, string[] args)
    {
        var color = args.First();
        return await _accountService.UpdateDisplayColorAsync(userId, new UpdateDisplayColorRequest(color));
    }
}
