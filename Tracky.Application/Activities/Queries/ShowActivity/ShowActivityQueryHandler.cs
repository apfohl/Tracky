using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Common;
using Tracky.Application.Persistence;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ShowActivity;

public sealed class ShowActivityQueryHandler(IUnitOfWork<ActivityReadModel> unitOfWork)
    : IQueryHandler<ShowActivityQuery, ActivityReadModel>
{
    public Task<Result<ActivityReadModel>> Handle(ShowActivityQuery command, CancellationToken cancellationToken) =>
        unitOfWork.Repository.Lookup(command.Id);
}
