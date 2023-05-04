namespace VKTestBackendTask.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddApiVersioning();
        
        return services;
    }

    private static void AddApiVersioning(this IServiceCollection services)
    {
        /*services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });*/
    }
}