using ChatApp.Server.Features.Chat;
using ChatApp.Server.Features.Chat.Channels;
using ChatApp.Server.Features.Chat.Commands;
using ChatApp.Server.Features.Chat.Messages;

namespace ChatApp.Server.Extensions;

public static class ChatServiceExtensions
{
    public static IServiceCollection AddChat(this IServiceCollection services)
    {
        services.AddScoped<IChannelService, ChannelService>();
        services.AddSingleton<IChannelMessageBuffer, ChannelMessageBuffer>();
        services.AddScoped<IChatService, ChatService>();

        return services;
    }

    public static IServiceCollection AddChatChannels(this IServiceCollection services)
    {
        services.AddSingleton<ChatChannel, GeneralChannel>();
        services.AddSingleton<ChatChannel, TradeChannel>();

        return services;
    }

    public static IServiceCollection AddChatCommands(this IServiceCollection services)
    {
        services.AddTransient<ICommandProcessor, CommandProcessor>();
        services.AddTransient<CommandDefinitionProvider>();

        services.AddTransient<IChatCommand, ChangeColorCommand>();
        services.AddTransient<IChatCommand, WhisperCommand>();

        return services;
    }
}
