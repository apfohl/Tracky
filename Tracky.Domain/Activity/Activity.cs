using JetBrains.Annotations;
using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.Validation;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId>
{
    private Result<ActivityState> State { get; set; } = ActivityState.Created;

    public Activity(ActivityId id, IEnumerable<DomainEvent> events) : base(id, events)
    {
    }

    public static Activity Create() =>
        new(ActivityId.CreateUnique(), Array.Empty<DomainEvent>());

    public Result<Activity> Start(string description) =>
        State
            .Bind(ActivityValidation.ValidateNotRunning)
            .Bind(ActivityValidation.ValidateNotEnded)
            .Map(_ => (Activity)ApplyDomainEvent(new ActivityStarted(description)));

    public Result<Activity> ChangeDescription(string description) =>
        (Result<Activity>)ApplyDomainEvent(new ActivityDescriptionChanged(description));

    public Result<Activity> Pause() =>
        State
            .Bind(ActivityValidation.ValidateRunning)
            .Bind(ActivityValidation.ValidateNotEnded)
            .Map(_ => (Activity)ApplyDomainEvent(new ActivityPaused(DateTime.Now)));

    public Result<Activity> Resume() =>
        State
            .Bind(ActivityValidation.ValidatePaused)
            .Map(_ => (Activity)ApplyDomainEvent(new ActivityResumed(DateTime.Now)));

    public Result<Activity> End() =>
        State
            .Bind(ActivityValidation.ValidateNotEnded)
            .Map(_ => (Activity)ApplyDomainEvent(new ActivityEnded()));

    [UsedImplicitly]
    internal Activity Apply(ActivityStarted @event)
    {
        State = ActivityState.Running;

        return this;
    }

    [UsedImplicitly]
    internal Activity Apply(ActivityDescriptionChanged @event) =>
        this;

    [UsedImplicitly]
    internal Activity Apply(ActivityPaused _)
    {
        State = ActivityState.Paused;

        return this;
    }

    [UsedImplicitly]
    internal Activity Apply(ActivityResumed _)
    {
        State = ActivityState.Running;

        return this;
    }

    [UsedImplicitly]
    internal Activity Apply(ActivityEnded _)
    {
        State = ActivityState.Ended;

        return this;
    }
}
