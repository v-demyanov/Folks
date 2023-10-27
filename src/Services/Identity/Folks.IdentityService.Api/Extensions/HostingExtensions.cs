using Microsoft.EntityFrameworkCore;

using Folks.IdentityService.Api.Constants;
using Folks.IdentityService.Infrastructure;

namespace Folks.IdentityService.Api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddLocalApiAuthentication();

        var connectionString = builder.Configuration.GetConnectionString("IdentityConnectionString");

        builder.Services.AddIdentityServer()
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

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseIdentityServer();

        return app;
    }
}
