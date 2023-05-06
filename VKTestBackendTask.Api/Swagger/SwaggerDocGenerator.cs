using Swashbuckle.AspNetCore.SwaggerGen;

namespace VKTestBackendTask.Api.Swagger;

/// <summary>
/// Class for setting swagger with custom settings
/// </summary>
public static class SwaggerDocGenerator
{
    public static void AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.AddXmlComments();
        });
        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    private static void AddXmlComments(this SwaggerGenOptions swagger)
    {
        // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        // swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
}