using ChatApp.Server.Features.Auth.Requests;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Auth;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<SignInResult> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}
