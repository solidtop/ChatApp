namespace ChatApp.Server.Features.Chat.Channels;

public record ChatChannelResponse(int Id, string Name, bool HasAccess)
{
    public static ChatChannelResponse FromChatChannel(ChatChannel channel, IEnumerable<string> userRoles)
    {
        var hasAccess = channel.AllowedRoles.Length == 0 ||
            channel.AllowedRoles.Intersect(userRoles, StringComparer.OrdinalIgnoreCase).Any();

        return new(channel.Id, channel.Name, hasAccess);
    }
}
