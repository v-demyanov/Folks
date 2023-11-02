using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Folks.IdentityService.Infrastructure.Persistence;

namespace Folks.IdentityService.Infrastructure;

public static class DatabaseInitializer
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        PerformMigrations(serviceScope.ServiceProvider);
        SeedIdentityConfigurationEntities(serviceScope.ServiceProvider);
    }

    private static void PerformMigrations(IServiceProvider services)
    {
        services.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
        services.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        services.GetRequiredService<IdentityServiceDbContext>().Database.Migrate();
    }

    private static void SeedIdentityConfigurationEntities(IServiceProvider services)
    {
        var context = services.GetRequiredService<ConfigurationDbContext>();

        ResetIdentityConfigurationEntities(context);

        if (!context.ApiScopes.Any())
        {
            foreach (var apiScope in Config.ApiScopes)
            {
                context.ApiScopes.Add(apiScope.ToEntity());
            }

            context.SaveChanges();
        }

        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }

            context.SaveChanges();
        }
    }

    private static void ResetIdentityConfigurationEntities(ConfigurationDbContext context)
    {
        context.ApiScopes.RemoveRange(context.ApiScopes);
        context.Clients.RemoveRange(context.Clients);
        context.SaveChanges();
    }
}
