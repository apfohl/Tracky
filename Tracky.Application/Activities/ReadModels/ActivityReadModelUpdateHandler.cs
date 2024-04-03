using MediatR;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.ReadModels;

public sealed class ActivityReadModelUpdateHandler(IRepository<ActivityReadModel> activityRepository)
    : INotificationHandler<ActivityReadModelUpdate>
{
    public async Task Handle(ActivityReadModelUpdate notification, CancellationToken cancellationToken)
    {
        foreach (var @event in notification.Events)
        {
            switch (@event)
            {
                case ActivityStarted e:
                    await activityRepository.InsertAsync(
                        new ActivityReadModel(notification.Id.ToString(), ActivityState.Running, e.Description));
                    break;
                case ActivityPaused:
                    await activityRepository.GetByIdAsync(notification.Id)
                        .BindAsync(
                            activity => activityRepository.UpdateAsync(activity with { State = ActivityState.Paused }));
                    break;
                case ActivityResumed:
                    await activityRepository.GetByIdAsync(notification.Id)
                        .BindAsync(
                            activity => activityRepository.UpdateAsync(activity with
                            {
                                State = ActivityState.Running
                            }));
                    break;
                case ActivityEnded:
                    await activityRepository.GetByIdAsync(notification.Id)
                        .BindAsync(
                            activity => activityRepository.UpdateAsync(activity with { State = ActivityState.Ended }));
                    break;
                case ActivityDescriptionChanged e:
                    await activityRepository.GetByIdAsync(notification.Id)
                        .BindAsync(
                            activity => activityRepository.UpdateAsync(activity with
                            {
                                Description = e.Description
                            }));
                    break;
            }
        }
    }
}
