using MediatR;
using Tracky.Application.Common;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.ResumeActivity;

public sealed class ResumeActivityCommandHandler(IRepository<Activity, ActivityId> repository)
    : ICommandHandler<ResumeActivityCommand>
{
    public Task<Result<Unit>> Handle(ResumeActivityCommand command, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(ActivityId.Create(command.Id))
            .BindAsync(activity => activity.Resume())
            .BindAsync(repository.SaveAsync)
            .MapAsync(_ => Unit.Value);
}
