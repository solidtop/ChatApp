using ChatApp.Server.Common.Results;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Chat.Commands;
using ChatApp.Server.Features.Chat.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Chat;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status200OK)]
[Route("api/chat")]
public class ChatController(
    IChannelService channelService,
    IMessageService messageService,
    CommandDefinitionProvider commandProvider) : ControllerBase
{
    private readonly IChannelService _channelService = channelService;
    private readonly IMessageService _messageService = messageService;
    private readonly CommandDefinitionProvider _commandProvider = commandProvider;

    [HttpGet("channels")]
    public ActionResult<List<ChatChannel>> GetChannels()
    {
        var channels = _channelService.GetAllChannels();
        return Ok(channels);
    }

    [HttpGet("channels/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ChatChannel> GetChannel(int id)
    {
        var result = _channelService.GetChannelById(id);
        return result.Match(Ok, ApiResults.Problem);
    }

    [HttpGet("channels/{id}/messages")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<ChatMessage>> GetMessages(int id)
    {
        var result = _messageService.GetLatestMessages(id);
        return result.Match(Ok, ApiResults.Problem);
    }

    [HttpGet("commands")]
    public ActionResult<List<CommandDefinition>> GetCommands()
    {
        return Ok(_commandProvider.GetDefinitions());
    }
}
