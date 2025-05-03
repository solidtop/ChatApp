using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Server.Features.Chat;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/chat")]
public class AdminChatController(IChatService chatService) : ControllerBase
{
    private readonly IChatService _chatService = chatService;

    [HttpPost("announcements")]
    public Task SendAnnouncement([FromBody] string content)
    {
        return _chatService.SendAnnouncementAsync(content);
    }
}
