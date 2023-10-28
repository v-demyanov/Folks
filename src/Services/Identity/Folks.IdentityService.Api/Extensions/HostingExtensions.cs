using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Folks.IdentityService.Infrastructure;
using Folks.IdentityService.Infrastructure.Constants;
using Folks.IdentityService.Domain.Entities;
using Folks.IdentityService.Infrastructure.Persistence;

namespace Folks.IdentityService.Api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddLocalApiAuthentication()
            .AddInfrastructureServices(builder.Configuration);

        builder.Services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityServiceDbContext>()
            .AddDefaultTokenProviders();

        var connectionString = builder.Configuration
            .GetConnectionString(EnvironmentSettings.ConnectionStringName);

        builder.Services
            .AddIdentityServer()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseNpgsql(connectionString, builder =>
                        builder.MigrationsAssembly(EnvironmentSettings.MigrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseNpgsql(connectionString, builder =>
                        builder.MigrationsAssembly(EnvironmentSettings.MigrationsAssembly));
            })
            .AddAspNetIdentity<User>();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        DatabaseInitializer.Initialize(app);

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseIdentityServer();

        return app;
    }
}
