using ChatApp.Server.Common.Exceptions;
using ChatApp.Server.Data;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Account;
using ChatApp.Server.Features.Avatars;
using ChatApp.Server.Features.Users;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDevCorsPolicy();
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthenticationServices();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddOpenApiWithConfig();

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAvatarService, AvatarService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCorsPolicy");
    app.ApplyMigrations();
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

    await app.Services.SeedRolesAsync();
    await app.Services.SeedAdminAsync();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

