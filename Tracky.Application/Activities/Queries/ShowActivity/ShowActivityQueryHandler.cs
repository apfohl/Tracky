using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Common;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ShowActivity;

public sealed class ShowActivityQueryHandler(IRepository<ActivityReadModel> repository)
    : IQueryHandler<ShowActivityQuery, ActivityReadModel>
{
    public Task<Result<ActivityReadModel>> Handle(ShowActivityQuery command, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(command.Id);
}
