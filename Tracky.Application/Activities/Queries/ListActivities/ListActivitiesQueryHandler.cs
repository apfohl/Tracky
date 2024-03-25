using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed class ListActivitiesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListActivitiesQuery, Result<IEnumerable<Activity>>>
{
    public Task<Result<IEnumerable<Activity>>> Handle(ListActivitiesQuery query, CancellationToken _) =>
        unitOfWork.Activities.ListAsync();
}
