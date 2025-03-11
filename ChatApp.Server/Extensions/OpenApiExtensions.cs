namespace ChatApp.Server.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiWithConfig(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Servers =
                [
                    new () { Url = "https://localhost:5001" }
                ];

                return Task.CompletedTask;
            });
        });

        return services;
    }
}
