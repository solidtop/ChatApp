using ChatApp.Server.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Avatars;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("api/[controller]")]
public class AvatarsController(IAvatarService avatarService) : ControllerBase
{
    private readonly IAvatarService _avatarService = avatarService;

    [HttpGet]
    public async Task<ActionResult<List<Avatar>>> GetAvatars()
    {
        var avatars = await _avatarService.GetAvatarsAsync();
        return Ok(avatars);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Avatar>>> GetAvatars(int id)
    {
        var result = await _avatarService.GetAvatarAsync(id);
        return result.Match(Ok, ApiResults.Problem);
    }
}
