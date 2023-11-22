using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using System.Reflection;

using MassTransit;

using Folks.ChatService.Api.Constants;
using Folks.ChatService.Infrastructure;
using Folks.ChatService.Application;
using Folks.ChatService.Api.Consumers;

namespace Folks.ChatService.Api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen((options) =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Folks.ChatService.Api",
                Version = "v1"
            });
        });

        builder.Services
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
                policy.RequireClaim("scope", "chatServiceApi");
            }));

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Folks.ChatService.Api v1"));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
