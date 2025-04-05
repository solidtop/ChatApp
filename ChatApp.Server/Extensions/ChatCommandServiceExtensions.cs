using ChatApp.Server.Features.Chat.Commands;

namespace ChatApp.Server.Extensions;

public static class ChatCommandServiceExtensions
{
    public static IServiceCollection AddChatCommands(this IServiceCollection services)
    {
        services.AddTransient<IChatCommandProcessor, ChatCommandProcessor>();
        services.AddTransient<ChatCommandDefinitionProvider>();

        services.AddTransient<IChatCommand, ChangeColorCommand>();

        return services;
    }
}
