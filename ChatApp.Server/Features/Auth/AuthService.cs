﻿using ChatApp.Server.Common.Utils;
using ChatApp.Server.Features.Auth.Requests;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Server.Features.Auth;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ColorGenerator colorGenerator) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ColorGenerator _colorGenerator = colorGenerator;

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var newUser = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            DisplayColor = _colorGenerator.Generate(),
            AvatarId = 1
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return result;
        }

        return await _userManager.AddToRoleAsync(newUser, "User");
    }

    public async Task<SignInResult> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return SignInResult.Failed;
        }

        return await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: true, lockoutOnFailure: false);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
