using ChatApp.Server.Common.Results;

namespace ChatApp.Server.Features.Avatars;

public static class AvatarErrors
{
    public static Error NotFound(int avatarId) =>
        new(ErrorType.NotFound, ErrorMessages.NotFound("Avatar", avatarId.ToString()));
}
