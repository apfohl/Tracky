using MediatR;
using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ShowActivity;

public sealed class ShowActivityQueryHandler(IRepository<ActivityReadModel> repository)
    : IRequestHandler<ShowActivityQuery, Result<ActivityReadModel>>
{
    public Task<Result<ActivityReadModel>> Handle(ShowActivityQuery command, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(command.Id);
}
