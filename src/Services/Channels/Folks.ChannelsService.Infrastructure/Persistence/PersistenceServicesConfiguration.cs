// Copyright (c) v-demyanov. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Folks.ChannelsService.Infrastructure.Persistence;

public static class PersistenceServicesConfiguration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("MongoDbConfig:ConnectionString");
        if (connectionString is null)
        {
            throw new NullReferenceException(nameof(connectionString));
        }

        var databaseName = configuration.GetValue<string>("MongoDbConfig:DatabaseName");
        if (databaseName is null)
        {
            throw new NullReferenceException(nameof(databaseName));
        }

        MongoClient? mongoClient = null;
        services.AddDbContext<ChannelsServiceDbContext>(options =>
        {
            if (mongoClient is null)
            {
                mongoClient = new MongoClient(connectionString);
            }

            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            options.UseMongoDB(mongoDatabase.Client, mongoDatabase.DatabaseNamespace.DatabaseName);
        });

        return services;
    }
}
