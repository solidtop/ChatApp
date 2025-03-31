using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Auth;
using ChatApp.Server.Features.Avatars;
using ChatApp.Server.Features.Chat;
using ChatApp.Server.Features.Chat.Channels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Avatar> Avatars { get; set; }
    public DbSet<ChatChannel> ChatChannels { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .Navigation(user => user.Avatar)
            .AutoInclude();

        builder.Entity<ChatMessage>()
             .ConfigureChatMessageRelationships();

        // Seed some placeholder avatars
        builder.Entity<Avatar>().HasData(
            new Avatar() { Id = 1, Name = "Default", ImageUrl = "https://www.gravatar.com/avatar/?d=mp" },
            new Avatar() { Id = 2, Name = "Ninja cat", ImageUrl = "https://robohash.org/9fd81b488a86a7b3f61eebbca767b644?set=set4&bgset=&size=200x200" },
            new Avatar() { Id = 3, Name = "Music cat", ImageUrl = "https://robohash.org/752b7f90146751c9174f154aa4063bea?set=set4&bgset=&size=200x200" }
        );

        // Seed chat channels
        builder.Entity<ChatChannel>().HasData(
            new ChatChannel() { Id = 1, Name = "global", AllowedRoles = [] },
            new ChatChannel() { Id = 2, Name = "admin", AllowedRoles = ["Admin"] }
        );
    }
}
