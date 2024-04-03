using MediatR;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.PauseActivity;

public sealed class PauseActivityCommandHandler(IRepository<Activity, ActivityId> repository)
    : IRequestHandler<PauseActivityCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(PauseActivityCommand command, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(ActivityId.Create(command.Id))
            .BindAsync(activity => activity.Pause())
            .BindAsync(repository.SaveAsync)
            .MapAsync(_ => Unit.Value);
}
