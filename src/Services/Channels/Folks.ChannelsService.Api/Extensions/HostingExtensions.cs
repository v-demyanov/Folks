using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using System.Reflection;

using MassTransit;

using Folks.ChannelsService.Infrastructure;
using Folks.ChannelsService.Application;
using Folks.ChannelsService.Api.Consumers;
using Folks.ChannelsService.Api.Hubs;
using Folks.ChannelsService.Api.Middlewares;
using Folks.ChannelsService.Api.Common.Constants;

namespace Folks.ChannelsService.Api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddSignalR();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen((options) =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Folks.ChannelsService.Api",
                Version = "v1"
            });
        });

        builder.Services
            .AddHttpContextAccessor()
            .AddInfrastructureServices(builder.Configuration)
            .AddApplicationServices();

        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumer<UserRegisteredConsumer>();

            config.UsingRabbitMq((context, config) =>
            {
                var hostAddress = builder.Configuration.GetValue<string>("EventBusConfig:HostAddress");
                config.Host(hostAddress);

                config.ReceiveEndpoint(config =>
                {
                    config.ConfigureConsumer<UserRegisteredConsumer>(context);
                });
            });
        });

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        var authority = builder.Configuration.GetValue<string>("AuthenticationOptions:Authority");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.TokenValidationParameters.ValidateAudience = false;
                options.RequireHttpsMetadata = false;
            });

        builder.Services.AddAuthorization(options =>
            options.AddPolicy(AuthorizationPolicies.ApiScope, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "channelsServiceApi");
            }));

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Folks.ChannelsService.Api v1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHub<ChannelsHub>(ApiRoutePatterns.ChannelsHub);
        app.MapControllers();

        return app;
    }
}
