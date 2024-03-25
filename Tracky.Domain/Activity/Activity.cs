using JetBrains.Annotations;
using MediatR;
using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Errors;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId, Guid>
{
    public string Description { get; private set; }
    public ActivityState State { get; private set; } = ActivityState.Created;

    public Activity(ActivityId id, IEnumerable<DomainEvent> events) : base(id, events)
    {
    }

    public static Activity Create() =>
        new(ActivityId.CreateUnique(), Array.Empty<DomainEvent>());

    public Result<Activity> Start(string description) =>
        ApplyDomainEvent(new ActivityStarted(Id, description)).Map(_ => this);

    public Result<Activity> ChangeDescription(string description) =>
        ApplyDomainEvent(new ActivityDescriptionChanged(Id, description)).Map(_ => this);

    public Result<Activity> Pause() => ApplyDomainEvent(new ActivityPaused(Id)).Map(_ => this);

    public Result<Activity> Resume() => ApplyDomainEvent(new ActivityResumed(Id)).Map(_ => this);

    public Result<Activity> End() => ApplyDomainEvent(new ActivityEnded(Id)).Map(_ => this);

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityStarted @event)
    {
        switch (State)
        {
            case ActivityState.Running:
                return new ActivityAlreadyStarted();
            case ActivityState.Ended:
                return new ActivityAlreadyEnded();
            case ActivityState.Paused:
                return new ActivityAlreadyStarted();
            case ActivityState.Created:
                Description = @event.Description;
                State = ActivityState.Running;
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityDescriptionChanged @event)
    {
        Description = @event.Description;
        return Unit.Value;
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityPaused _)
    {
        switch (State)
        {
            case ActivityState.Running:
                State = ActivityState.Paused;
                return Unit.Value;
            case ActivityState.Ended:
                return new ActivityAlreadyEnded();
            case ActivityState.Paused:
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityResumed _)
    {
        switch (State)
        {
            case ActivityState.Running:
                return Unit.Value;
            case ActivityState.Ended:
                return new ActivityAlreadyEnded();
            case ActivityState.Paused:
                State = ActivityState.Running;
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityEnded _)
    {
        switch (State)
        {
            case ActivityState.Running:
                State = ActivityState.Ended;
                return Unit.Value;
            case ActivityState.Ended:
                return Unit.Value;
            case ActivityState.Paused:
                State = ActivityState.Ended;
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }
}
