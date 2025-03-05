namespace ChatApp.Server.Extensions;

public static class CorsServiceExtensions
{
    public static IServiceCollection AddDevCorsPolicy(this IServiceCollection services)
    {
        return services.AddCors(o => o.AddPolicy("DevCorsPolicy", policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));
    }
}
