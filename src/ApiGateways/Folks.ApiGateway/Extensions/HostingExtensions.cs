// Copyright (c) v-demyanov. All rights reserved.

using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Folks.ApiGateway.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        var authenticationProviderKey = "IdentityServerApiKey";
        var authority = builder.Configuration.GetValue<string>("AuthenticationOptions:Authority");

        builder.Services.AddAuthentication(authenticationProviderKey)
            .AddJwtBearer(authenticationProviderKey, options =>
            {
                options.Authority = authority;
                options.TokenValidationParameters.ValidateAudience = false;
                options.RequireHttpsMetadata = false;
            });

        builder.Services
            .AddOcelot()
            .AddCacheManager(settings => settings.WithDictionaryHandle());
        builder.Services.AddSignalR();

        return builder;
    }

    public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseWebSockets();
        await app.UseOcelot();

        return app;
    }
}
