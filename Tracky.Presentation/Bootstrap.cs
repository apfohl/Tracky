using EventStore.Client;
using MongoDB.Driver;

namespace Tracky.Presentation;

public static class Bootstrap
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) =>
        services
            .AddSingleton(provider => new EventStoreClient(EventStoreClientSettings.Create(
                provider.GetService<IConfiguration>().GetConnectionString("EventStore")!)))
            .AddSingleton<IMongoClient>(provider =>
                new MongoClient(provider.GetService<IConfiguration>().GetConnectionString("MongoDb")));
}
