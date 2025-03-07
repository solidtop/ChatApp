using ChatApp.Server.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Users;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserSummary>>> GetUserSummaries()
    {
        var summaries = await _userService.GetUserSummaries();
        return Ok(summaries);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDetails>> GetUserDetails(string id)
    {
        var result = await _userService.GetUserDetails(id);
        return result.Match(Ok, ApiResults.Problem);
    }
}
