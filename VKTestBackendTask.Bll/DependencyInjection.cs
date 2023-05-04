using Microsoft.Extensions.DependencyInjection;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Bll.Services.Implementations;

namespace VKTestBackendTask.Bll;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}