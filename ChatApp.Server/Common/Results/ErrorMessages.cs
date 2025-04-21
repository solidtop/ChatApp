namespace ChatApp.Server.Common.Results;

public static class ErrorMessages
{
    public static string NotFound(string entityName, string entityId) => $"{entityName} with id '{entityId}' not found.";
}
