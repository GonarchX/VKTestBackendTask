using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VKTestBackendTask.Bll.Options;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Bll.Services.Implementations;

namespace VKTestBackendTask.Bll;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.Configure<HashingOptions>(configuration.GetSection(nameof(HashingOptions)));
        
        return services;
    }
}