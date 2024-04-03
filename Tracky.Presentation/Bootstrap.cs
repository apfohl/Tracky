using EventStore.Client;
using MongoDB.Driver;

namespace Tracky.Presentation;

public static class Bootstrap
{
    private const string ConnectionString =
        "esdb+discover://localhost:2113?tls=false&keepAliveTimeout=10000&keepAliveInterval=10000";

    public static IServiceCollection AddPresentation(this IServiceCollection services) =>
        services
            .AddSingleton(_ => new EventStoreClient(EventStoreClientSettings.Create(ConnectionString)))
            .AddSingleton(_ => new MongoClient("mongodb://localhost:27017"));
}
