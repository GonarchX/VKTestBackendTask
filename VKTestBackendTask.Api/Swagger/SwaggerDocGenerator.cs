using System.Reflection;
using Microsoft.OpenApi.Models;
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
            swagger.AddBasicAuthentication();
            swagger.AddXmlComments();
        });
        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    private static void AddBasicAuthentication(this SwaggerGenOptions swagger)
    {
        swagger.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter your credentials",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic"
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Basic"
                    }
                },
                Array.Empty<string>()
            }
        });
    }

    private static void AddXmlComments(this SwaggerGenOptions swagger)
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
}