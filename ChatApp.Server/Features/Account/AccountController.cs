using System.Security.Claims;
using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Account.Requests;
using ChatApp.Server.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Account;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("/api/account")]
public class AccountController(IAccountService accountService, IUserService userService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;
    private readonly IUserService _userService = userService;

    [HttpGet("user")]
    public async Task<ActionResult<UserProfile>> GetUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _userService.GetUserProfile(userId);
        return result.Match(Ok, ApiResults.Problem);
    }

    [HttpPut("display-color")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateDisplayColor([FromBody] UpdateDisplayColorRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _accountService.UpdateDisplayColorAsync(userId, request);
        return result.MatchVoid(Ok, ApiResults.Problem);
    }

    [HttpPut("avatar")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAvatar([FromBody] UpdateAvatarRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _accountService.UpdateAvatarAsync(userId, request);
        return result.MatchVoid(Ok, ApiResults.Problem);
    }
}
