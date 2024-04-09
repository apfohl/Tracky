using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Common;
using Tracky.Application.Persistence;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed class ListActivitiesQueryHandler(IUnitOfWork<ActivityReadModel> unitOfWork)
    : IQueryHandler<ListActivitiesQuery, IEnumerable<ActivityReadModel>>
{
    public Task<Result<IEnumerable<ActivityReadModel>>> Handle(ListActivitiesQuery query, CancellationToken _) =>
        unitOfWork.Repository.All(_ => true);
}
