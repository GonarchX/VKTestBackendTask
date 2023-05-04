using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VKTestBackendTask.Dal.Repositories.Abstractions;
using VKTestBackendTask.Dal.Repositories.Implementations;

namespace VKTestBackendTask.Dal;

public static class DependencyInjection
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:Default"];
        services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(connectionString); });
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}