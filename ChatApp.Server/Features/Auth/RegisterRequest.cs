namespace ChatApp.Server.Features.Auth;

public record RegisterRequest(string Username, string Email, string Password);
