using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Persistence;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
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
            .AddScoped<MongoDbContext<ActivityReadModel>>()
            .AddScoped<IUnitOfWork<ActivityReadModel>, MongoDbUnitOfWork<ActivityReadModel>>()
            .AddTransient<IRepository<Activity, ActivityId>, EventStoreDbRepository<Activity, ActivityId>>();
}
