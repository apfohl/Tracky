using System.Reactive;
using JetBrains.Annotations;
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

    public Result<Unit> ChangeDescription(string description) =>
        ApplyDomainEvent(new ActivityDescriptionChanged(description));

    public Result<Unit> Pause() =>
        ApplyDomainEvent(new ActivityPaused());

    public Result<Unit> Resume() =>
        ApplyDomainEvent(new ActivityResumed());

    public Result<Unit> End() =>
        ApplyDomainEvent(new ActivityEnded());

    public static Activity Start(string description) =>
        new(ActivityId.CreateUnique(), new[] { new ActivityStarted(description) });

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityStarted @event)
    {
        switch (State.Name)
        {
            case nameof(ActivityState.Running):
                return new ActivityAlreadyStarted();
            case nameof(ActivityState.Ended):
                return new ActivityAlreadyEnded();
            case nameof(ActivityState.Paused):
                return new ActivityAlreadyStarted();
            default:
                Description = @event.Description;
                State = ActivityState.Running;
                return Unit.Default;
        }
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityDescriptionChanged @event)
    {
        Description = @event.Description;
        return Unit.Default;
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityPaused _)
    {
        switch (State.Name)
        {
            case nameof(ActivityState.Running):
                State = ActivityState.Paused;
                return Unit.Default;
            case nameof(ActivityState.Ended):
                return new ActivityAlreadyEnded();
            case nameof(ActivityState.Paused):
                return Unit.Default;
            default:
                throw new ArgumentException(nameof(State.Name));
        }
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityResumed _)
    {
        switch (State.Name)
        {
            case nameof(ActivityState.Running):
                return Unit.Default;
            case nameof(ActivityState.Ended):
                return new ActivityAlreadyEnded();
            case nameof(ActivityState.Paused):
                State = ActivityState.Running;
                return Unit.Default;
            default:
                throw new ArgumentException(nameof(State.Name));
        }
    }

    [UsedImplicitly]
    internal Result<Unit> Apply(ActivityEnded _)
    {
        switch (State.Name)
        {
            case nameof(ActivityState.Running):
                State = ActivityState.Ended;
                return Unit.Default;
            case nameof(ActivityState.Ended):
                return Unit.Default;
            case nameof(ActivityState.Paused):
                State = ActivityState.Ended;
                return Unit.Default;
            default:
                throw new ArgumentException(nameof(State.Name));
        }
    }
}
