using MediatR;
using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed class ListActivitiesQueryHandler(IRepository<ActivityReadModel> activityRepository)
    : IRequestHandler<ListActivitiesQuery, Result<IEnumerable<ActivityReadModel>>>
{
    public Task<Result<IEnumerable<ActivityReadModel>>> Handle(ListActivitiesQuery query, CancellationToken _) =>
        activityRepository.FindAllAsync(_ => true);
}
