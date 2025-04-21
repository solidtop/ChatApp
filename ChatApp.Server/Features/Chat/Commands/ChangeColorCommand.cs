using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Account;
using ChatApp.Server.Features.Account.Requests;

namespace ChatApp.Server.Features.Chat.Commands;

public class ChangeColorCommand(IAccountService accountService) : IChatCommand
{
    private readonly IAccountService _accountService = accountService;

    public string Name => "color";
    public string Description => "Change your display color.";
    public string Usage => "/color <newColor>\nExample: /color #FF5733";

    public async Task<Result<string>> ExecuteAsync(string userId, string[] args)
    {
        var color = args.First();
        var result = await _accountService.UpdateDisplayColorAsync(userId, new UpdateDisplayColorRequest(color));

        if (result.IsFailure)
        {
            string errorMessage;

            if (result.Error is ValidationError validationError)
            {
                errorMessage = validationError.Errors.First();
            }
            else
            {
                errorMessage = result.Error.Message;
            }

            return Result.Fail<string>(errorMessage);
        }


        return Result.Ok("Your display color has been changed.");
    }
}
