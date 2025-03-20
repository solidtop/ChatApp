using ChatApp.Server.Features.Chat;

namespace ChatApp.Server.Extensions;

public static class HubEndpointExtensions
{
    public static IEndpointRouteBuilder MapHubs(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<ChatHub>("/chathub");

        return endpoints;
    }
}
