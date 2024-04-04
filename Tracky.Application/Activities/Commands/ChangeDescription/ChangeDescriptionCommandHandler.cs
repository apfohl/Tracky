using MediatR;
using Tracky.Application.Common;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.ChangeDescription;

public sealed class ChangeDescriptionCommandHandler(IRepository<Activity, ActivityId> repository)
    : ICommandHandler<ChangeDescriptionCommand>
{
    public Task<Result<Unit>> Handle(ChangeDescriptionCommand command, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(ActivityId.Create(command.Id))
            .BindAsync(activity => activity.ChangeDescription(command.Description))
            .BindAsync(repository.SaveAsync)
            .MapAsync(_ => Unit.Value);
}
