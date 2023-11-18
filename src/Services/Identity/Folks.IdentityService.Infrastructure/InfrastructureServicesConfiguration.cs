using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Folks.IdentityService.Infrastructure.Persistence;
using Folks.IdentityService.Infrastructure.Constants;
using Folks.IdentityService.Infrastructure.Models;

namespace Folks.IdentityService.Infrastructure;

public static class InfrastructureServicesConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentityServerConfig>(configuration.GetSection(nameof(IdentityServerConfig)));

        var connectionString = configuration
            .GetConnectionString(EnvironmentSettings.ConnectionStringName);

        services.AddDbContext<IdentityServiceDbContext>(options =>
            options.UseNpgsql(connectionString, options =>
                options.MigrationsAssembly(EnvironmentSettings.MigrationsAssembly)));

        return services;
    }
}
