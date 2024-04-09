using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Persistence;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Infrastructure.EventStore;
using Tracky.Infrastructure.Repositories;
using Tracky.Infrastructure.UnitOfWork;

namespace Tracky.Infrastructure;

public static class Bootstrap
{
    private const string ReadModelsDatabaseName = "ReadModels";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSingleton(provider => provider.GetService<IMongoClient>().GetDatabase(ReadModelsDatabaseName))
            .AddTransient<IEventStore, EventStoreDb>()
            .AddTransient<MongoDbContext<ActivityReadModel>>()
            .AddTransient<IUnitOfWork<ActivityReadModel>, MongoDbUnitOfWork<ActivityReadModel>>()
            .AddTransient<IRepository<Activity, ActivityId>, EventStoreDbRepository<Activity, ActivityId>>();
}
