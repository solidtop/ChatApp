namespace ChatApp.Server.Features.Chat.Channels;

public class GeneralChannel : ChatChannel
{
    public override int Id => 1;
    public override string Name => "General";
    public override string Description => "This is a general channel";
}
