using ChatApp.Server.Common.Results;
using ChatApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Features.Avatars;

public class AvatarService(ApplicationDbContext context) : IAvatarService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Avatar>> GetAvatarsAsync()
    {
        return await _context.Avatars.ToListAsync();
    }

    public async Task<Result<Avatar>> GetAvatarAsync(int avatarId)
    {
        var avatar = await _context.Avatars.FindAsync(avatarId);

        if (avatar is null)
        {
            return AvatarErrors.NotFound(avatarId);
        }

        return Result.Ok(avatar);
    }
}
