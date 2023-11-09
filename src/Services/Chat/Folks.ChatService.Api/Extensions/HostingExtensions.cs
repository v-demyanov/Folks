using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Folks.ChatService.Api.Constants;

namespace Folks.ChatService.Api.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen((options) =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Folks.ChatService.Api",
                Version = "v1"
            });
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

        return app;
    }
}
