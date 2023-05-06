using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using VKTestBackendTask.Api.Common.Errors;
using VKTestBackendTask.Api.Common.Mapping;
using VKTestBackendTask.Api.Swagger;

namespace VKTestBackendTask.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCustomSwaggerGen();
        services.AddApiVersioning();
        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
        services.AddMappings();
        
        return services;
    }

    private static void AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });
    }
}