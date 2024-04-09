using EventStore.Client;
using MongoDB.Driver;

namespace Tracky.Presentation;

public static class Bootstrap
{
    private const string EventStoreConnectionString =
        "esdb+discover://localhost:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000";

    private const string MongoDbConnectionString = "mongodb://localhost:27017/?directConnection=true";

    public static IServiceCollection AddPresentation(this IServiceCollection services) =>
        services
            .AddSingleton(_ => new EventStoreClient(EventStoreClientSettings.Create(EventStoreConnectionString)))
            .AddSingleton<IMongoClient>(_ => new MongoClient(MongoDbConnectionString));
}
