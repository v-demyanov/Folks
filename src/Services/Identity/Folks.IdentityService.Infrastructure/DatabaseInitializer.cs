using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    }

    private static void SeedIdentityConfigurationEntities(IServiceProvider services)
    {
        var context = services.GetRequiredService<ConfigurationDbContext>();

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }

            context.SaveChanges();
        }
    }
}
