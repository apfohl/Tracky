using MediatR;
using Tracky.Application.Common;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.EndActivity;

public sealed class EndActivityCommandHandler(IRepository<Activity, ActivityId> repository)
    : ICommandHandler<EndActivityCommand>
{
    public Task<Result<Unit>> Handle(EndActivityCommand command, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(ActivityId.Create(command.Id))
            .BindAsync(activity => activity.End())
            .BindAsync(repository.SaveAsync)
            .MapAsync(_ => Unit.Value);
}
