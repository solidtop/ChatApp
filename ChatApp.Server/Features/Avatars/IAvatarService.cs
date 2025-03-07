using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Avatars;

public interface IAvatarService
{
    Task<IEnumerable<Avatar>> GetAvatarsAsync();
    Task<Result<Avatar>> GetAvatarAsync(int avatarId);
}
