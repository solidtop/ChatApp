namespace ChatApp.Server.Features.Auth.Requests;

public record RegisterRequest(string Username, string Email, string Password);
