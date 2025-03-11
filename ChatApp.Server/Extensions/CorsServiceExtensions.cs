namespace ChatApp.Server.Extensions;

public static class CorsServiceExtensions
{
    public static IServiceCollection AddDevCorsPolicy(this IServiceCollection services)
    {
        return services.AddCors(o => o.AddPolicy("DevCorsPolicy", policy =>
        {
            policy.WithOrigins("https://localhost:63332")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        }));
    }
}
