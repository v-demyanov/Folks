using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

using Folks.ChannelsService.Infrastructure.Persistence;

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

        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(databaseName);

        services.AddDbContext<ChatServiceDbContext>(options =>
            options.UseMongoDB(mongoDatabase.Client, mongoDatabase.DatabaseNamespace.DatabaseName));

        return services;
    }
}
