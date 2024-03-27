using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Activity;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.StartActivity;

public sealed class StartActivityCommandHandler(IRepository<Activity, ActivityId, Guid> activityRepository)
    : IRequestHandler<StartActivityCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(StartActivityCommand command, CancellationToken _) =>
        Activity.Create().ToResult()
            .Bind(activity => activity.Start(command.Description))
            .BindAsync(activityRepository.SaveAsync);
}
