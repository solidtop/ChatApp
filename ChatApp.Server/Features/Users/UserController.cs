using ChatApp.Server.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Users;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult<List<UserSummary>>> GetUsers()
    {
        var summaries = await _userService.GetUserSummaries();
        return Ok(summaries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserProfile>> GetUser(string id)
    {
        var result = await _userService.GetUserProfile(id);
        return result.Match(Ok, ApiResults.Problem);
    }
}

