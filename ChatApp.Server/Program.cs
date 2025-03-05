using ChatApp.Server.Data;
using ChatApp.Server.Extensions;
using ChatApp.Server.Features.Account;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDevCorsPolicy();
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddAuthenticationServices();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });

    await app.Services.SeedRolesAsync();
    await app.Services.SeedAdminAsync();

    app.UseCors("DevCorsPolicy");
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

