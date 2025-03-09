namespace ChatApp.Server.Common.Results;

public record ValidationError(string Message, List<string> Errors) : Error(ErrorType.Validation, Message);
