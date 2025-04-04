using ChatApp.Server.Features.Chat.Commands;

namespace ChatApp.Server.Extensions;

public static class ChatCommandServiceExtensions
{
    public static IServiceCollection AddChatCommands(this IServiceCollection services)
    {
        services.AddTransient<IChatCommand, ChangeColorCommand>();
        services.AddTransient<IChatCommandProcessor, ChatCommandProcessor>();

        return services;
    }
}
