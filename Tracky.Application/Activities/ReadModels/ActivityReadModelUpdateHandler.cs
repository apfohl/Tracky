using MediatR;
using Tracky.Application.Persistence;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.ReadModels;

public sealed class ActivityReadModelUpdateHandler(IUnitOfWork<ActivityReadModel> unitOfWork)
    : INotificationHandler<ActivityReadModelUpdate>
{
    public async Task Handle(ActivityReadModelUpdate notification, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository;

        foreach (var @event in notification.Events)
        {
            switch (@event)
            {
                case ActivityStarted e:
                    repository.Insert(
                        new ActivityReadModel(notification.Id.ToString(), ActivityState.Running, e.Description));
                    break;
                case ActivityPaused:
                    await repository.Lookup(notification.Id)
                        .TapAsync(activity =>
                            repository.Update(activity with { State = ActivityState.Paused }));
                    break;
                case ActivityResumed:
                    await repository.Lookup(notification.Id)
                        .TapAsync(
                            activity => repository.Update(activity with
                            {
                                State = ActivityState.Running
                            }));
                    break;
                case ActivityEnded:
                    await repository.Lookup(notification.Id)
                        .TapAsync(
                            activity => repository.Update(activity with { State = ActivityState.Ended }));
                    break;
                case ActivityDescriptionChanged e:
                    await repository.Lookup(notification.Id)
                        .TapAsync(
                            activity => repository.Update(activity with
                            {
                                Description = e.Description
                            }));
                    break;
            }
        }

        await unitOfWork.SaveChanges();
    }
}
