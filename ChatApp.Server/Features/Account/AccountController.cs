﻿using System.Security.Claims;
using ChatApp.Server.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Account;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("/api/[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    private readonly IAccountService _accountService = accountService;

    [HttpGet("details")]
    public async Task<ActionResult<AccountDetails>> GetAccountDetails()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _accountService.GetAccountDetailsAsync(userId);
        return result.Match(Ok, ApiResults.Problem);
    }

    [HttpPut("avatar")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAvatar([FromBody] UpdateAvatarRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _accountService.UpdateAvatarAsync(userId, request.AvatarId);
        return result.MatchVoid(Ok, ApiResults.Problem);
    }
}
