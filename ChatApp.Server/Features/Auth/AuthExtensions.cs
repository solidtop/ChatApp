using ChatApp.Server.Data;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Auth;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IAuthService, AuthService>();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "token";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
        });

        return services;
    }
}
