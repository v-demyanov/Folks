using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Folks.IdentityService.Infrastructure.Persistence;
using Folks.IdentityService.Infrastructure.Constants;

namespace Folks.IdentityService.Infrastructure;

public static class InfrastructureServicesConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString(EnvironmentSettings.ConnectionStringName);

        services.AddDbContext<IdentityServiceDbContext>(options =>
            options.UseNpgsql(connectionString, options =>
                options.MigrationsAssembly(EnvironmentSettings.MigrationsAssembly)));

        return services;
    }
}
