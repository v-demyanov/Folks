// Copyright (c) v-demyanov. All rights reserved.

using System.Reflection;

using FluentValidation;

using Folks.ChannelsService.Application.Behaviours;

using Microsoft.Extensions.DependencyInjection;

namespace Folks.ChannelsService.Application;

public static class ApplicationServicesConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
