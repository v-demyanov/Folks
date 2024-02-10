// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Infrastructure.Persistence;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Folks.ChannelsService.Infrastructure;

public static class InfrastructureServicesConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);

        return services;
    }
}
