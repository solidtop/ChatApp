﻿using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Auth;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var newUser = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
        };

        return await _userManager.CreateAsync(newUser, request.Password);
    }

    public async Task<SignInResult> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return SignInResult.Failed;
        }

        return await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
    }
}
