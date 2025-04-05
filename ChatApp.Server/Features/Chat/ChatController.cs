using System.Security.Claims;
using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Chat.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Chat;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("/api/chat")]
public class ChatController(IChatService chatService, ChatCommandDefinitionProvider commandProvider) : ControllerBase
{
    private readonly IChatService _chatService = chatService;
    private readonly ChatCommandDefinitionProvider _commandProvider = commandProvider;

    [HttpGet("channels")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ChatChannelResponse>>> GetChannels()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var channels = await _chatService.GetChannelsAsync(userId);
        return Ok(channels);
    }

    [HttpGet("channels/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ChatChannel>> GetChannel(int id)
    {
        var result = await _chatService.GetChannelAsync(id);
        return result.Match(Ok, ApiResults.Problem);
    }

    [HttpGet("channels/{id}/messages")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ChatChannel>> GetChannelMessages(int id, [FromQuery] int count = 10)
    {
        var messages = await _chatService.GetLatestMessagesAsync(id, count);
        return Ok(messages);
    }

    [HttpPost("messages")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ChatMessageResponse>> CreateMessage(ChatMessageRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await _chatService.CreateMessageAsync(userId, request);

        if (result.IsFailure)
        {
            return ApiResults.Problem(result);
        }

        var message = result.Value;

        return CreatedAtAction(nameof(CreateMessage), new { id = message?.Id }, message);
    }

    [HttpGet("commands")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ChatCommandDefinition> GetCommands()
    {
        return Ok(_commandProvider.GetDefinitions());
    }
}
