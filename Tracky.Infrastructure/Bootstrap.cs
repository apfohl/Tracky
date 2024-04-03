using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Persistence.Events;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Infrastructure.EventStore;
using Tracky.Infrastructure.Repositories;

namespace Tracky.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSingleton(provider => provider.GetService<MongoClient>().GetDatabase("ReadModels"))
            .AddTransient<IEventStore, EventStoreDb>()
            .AddTransient<IRepository<ActivityReadModel>, MongoDbRepository<ActivityReadModel>>()
            .AddTransient<IRepository<Activity, ActivityId>, EventStoreDbRepository<Activity, ActivityId>>();
}
