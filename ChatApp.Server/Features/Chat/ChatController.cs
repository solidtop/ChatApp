using ChatApp.Server.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Chat;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("/api/chat")]
public class ChatController(IChatService chatService) : ControllerBase
{
    private readonly IChatService _chatService = chatService;

    [HttpGet("channels")]
    public async Task<ActionResult<List<ChatChannel>>> GetChannels()
    {
        var channels = await _chatService.GetChannelsAsync();
        return Ok(channels);
    }

    [HttpGet("channels/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChatChannel>> GetChannel(int id)
    {
        var result = await _chatService.GetChannelAsync(id);
        return result.Match(Ok, ApiResults.Problem);
    }
}
