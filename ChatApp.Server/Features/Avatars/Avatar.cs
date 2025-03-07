namespace ChatApp.Server.Features.Avatars;

public class Avatar
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
}
