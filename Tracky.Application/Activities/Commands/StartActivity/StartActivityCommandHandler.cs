using Tracky.Application.Common;
using Tracky.Application.Persistence.Events;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.StartActivity;

public sealed class StartActivityCommandHandler(IRepository<Activity, ActivityId> activityRepository)
    : ICommandHandler<StartActivityCommand, ActivityId>
{
    public Task<Result<ActivityId>> Handle(StartActivityCommand command, CancellationToken _) =>
        Activity.Create().ToResult()
            .Bind(activity => activity.Start(command.Description))
            .BindAsync(activityRepository.SaveAsync);
}
