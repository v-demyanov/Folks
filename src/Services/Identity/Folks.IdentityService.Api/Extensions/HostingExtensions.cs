using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using System.Reflection;

using MassTransit;

using Folks.IdentityService.Infrastructure;
using Folks.IdentityService.Infrastructure.Constants;
using Folks.IdentityService.Domain.Entities;
using Folks.IdentityService.Infrastructure.Persistence;
using Folks.IdentityService.Application;

namespace Folks.IdentityService.Api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLocalApiAuthentication();

        builder.Services
            .AddInfrastructureServices(builder.Configuration)
            .AddApplicationServices();

        builder.Services.AddRazorPages();

        builder.Services
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityServiceDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        var connectionString = builder.Configuration
            .GetConnectionString(EnvironmentSettings.ConnectionStringName);

        builder.Services
            .AddIdentityServer((options) =>
            {
                var identityServerConfig = builder.Configuration
                    .GetSection(nameof(IdentityServerConfig))
                    .Get<IdentityServerConfig>();
                options.IssuerUri = identityServerConfig?.IssuerUri;
            })
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

        builder.Services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, config) =>
            {
                var hostAddress = builder.Configuration.GetValue<string>("EventBusConfig:HostAddress");
                config.Host(hostAddress);
            });
        });

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        DatabaseInitializer.Initialize(app);

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
           .RequireAuthorization();

        app.MapControllers();

        return app;
    }
}
