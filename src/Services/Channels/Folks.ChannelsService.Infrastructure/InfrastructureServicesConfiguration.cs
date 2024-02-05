// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

namespace Folks.ChannelsService.Infrastructure;

public static class InfrastructureServicesConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMongoDatabase(configuration);

        return services;
    }

    private static IServiceCollection ConfigureMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetValue<string>("MongoDbConfig:ConnectionString");

        var databaseName = configuration
            .GetValue<string>("MongoDbConfig:DatabaseName");

        if (connectionString is null)
        {
            throw new NullReferenceException(nameof(connectionString));
        }

        if (databaseName is null)
        {
            throw new NullReferenceException(nameof(databaseName));
        }

        services.AddDbContext<ChannelsServiceDbContext>(options =>
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            options.UseMongoDB(mongoDatabase.Client, mongoDatabase.DatabaseNamespace.DatabaseName);
        });

        return services;
    }
}
