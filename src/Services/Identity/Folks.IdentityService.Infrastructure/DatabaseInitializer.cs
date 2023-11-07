using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Folks.IdentityService.Infrastructure.Persistence;
using Folks.IdentityService.Infrastructure.Models;

namespace Folks.IdentityService.Infrastructure;

public static class DatabaseInitializer
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

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

        var identityConfig = services.GetRequiredService<IOptions<IdentityServerConfig>>().Value;

        SeedApiScopes(context, identityConfig.ApiScopes);
        SeedIdentityResources(context, identityConfig.IdentityResources);
        SeedClients(context, identityConfig.Clients);
    }

    private static void ResetIdentityConfigurationEntities(ConfigurationDbContext context)
    {
        context.ApiScopes.RemoveRange(context.ApiScopes);
        context.IdentityResources.RemoveRange(context.IdentityResources);
        context.Clients.RemoveRange(context.Clients);

        context.SaveChanges();
    }

    private static void SeedApiScopes(ConfigurationDbContext context, IEnumerable<ApiScope> apiScopes)
    {
        foreach (var apiScope in apiScopes)
        {
            context.ApiScopes.Add(apiScope.ToEntity());
        }
        context.SaveChanges();
    }

    private static void SeedIdentityResources(ConfigurationDbContext context, IEnumerable<IdentityResource> identityResources)
    {
        foreach (var identityResource in identityResources)
        {
            context.IdentityResources.Add(identityResource.ToEntity());
        }
        context.SaveChanges();
    }

    private static void SeedClients(ConfigurationDbContext context, IEnumerable<Client> clients)
    {
        foreach (var client in clients)
        {
            context.Clients.Add(client.ToEntity());
        }
        context.SaveChanges();
    }
}
