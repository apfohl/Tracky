using MediatR;
using Tracky.Domain.Common;
using Tracky.ReadModels.Activities;
using Tracky.ReadModels.Persistence;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed class ListActivitiesQueryHandler(IRepository<ActivityReadModel> activityRepository)
    : IRequestHandler<ListActivitiesQuery, Result<IEnumerable<ActivityReadModel>>>
{
    public Task<Result<IEnumerable<ActivityReadModel>>> Handle(ListActivitiesQuery query, CancellationToken _) =>
        activityRepository.FindAllAsync(_ => true);
}
