using MessagePack;

namespace ChatApp.Server.Extensions;

public static class SignalRExtensions
{
    public static IServiceCollection AddSignalRWithConfig(this IServiceCollection services)
    {
        services.AddSignalR()
            .AddMessagePackProtocol(options =>
            {
                options.SerializerOptions = MessagePackSerializerOptions.Standard
                .WithSecurity(MessagePackSecurity.UntrustedData);
            });

        return services;
    }
}
