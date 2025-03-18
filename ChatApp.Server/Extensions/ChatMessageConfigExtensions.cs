using ChatApp.Server.Features.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Extensions;

public static class ChatMessageConfigExtensions
{
    public static void ConfigureChatMessageRelationships(this EntityTypeBuilder<ChatMessage> builder)
    {
        builder.HasOne(m => m.Channel)
           .WithMany()
           .HasForeignKey(m => m.ChannelId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(m => m.User).AutoInclude();
    }
}
