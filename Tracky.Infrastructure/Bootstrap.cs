using Microsoft.Extensions.DependencyInjection;
using Tracky.Application.Interfaces;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Infrastructure.Repositories;
using Tracky.ReadModels.Activities;
using Tracky.ReadModels.Persistence;

namespace Tracky.Infrastructure;

public static class Bootstrap
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddTransient<IRepository<ActivityReadModel>, MongoDatabaseRepository<ActivityReadModel>>()
            .AddTransient<IRepository<Activity, ActivityId, Guid>, EventStoreRepository<Activity, ActivityId, Guid>>();
}
