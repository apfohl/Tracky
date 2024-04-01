using Microsoft.Extensions.DependencyInjection;
using Tracky.Application.Interfaces;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Infrastructure.EventStore;
using Tracky.Infrastructure.Repositories;
using Tracky.ReadModels.Activities;
using Tracky.ReadModels.Persistence;

namespace Tracky.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddTransient<IEventStore, EventStoreDb>()
            .AddTransient<IRepository<ActivityReadModel>, MongoDbRepository<ActivityReadModel>>()
            .AddTransient<IRepository<Activity, ActivityId>, EventStoreDbRepository<Activity, ActivityId>>();
}
