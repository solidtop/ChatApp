namespace ChatApp.Server.Common.Results;

public record Error(ErrorType Type, string Message)
{
    public static Error None => new(ErrorType.None, string.Empty);
}
