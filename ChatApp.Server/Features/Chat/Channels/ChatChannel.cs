namespace ChatApp.Server.Features.Chat.Channels;

public class ChatChannel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string[] AllowedRoles { get; set; } = [];
}
