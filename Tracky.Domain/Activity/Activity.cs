using JetBrains.Annotations;
using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId, Guid>
{
    public string Description { get; private set; }
    public ActivityState State { get; private set; }

    public Activity(ActivityId id, IEnumerable<DomainEvent> events) : base(id, events)
    {
    }

    public void ChangeDescription(string description) =>
        ApplyDomainEvent(new ActivityDescriptionChanged(description));

    public void Pause() =>
        ApplyDomainEvent(new ActivityPaused());

    public void Resume() =>
        ApplyDomainEvent(new ActivityResumed());

    public void End() =>
        ApplyDomainEvent(new ActivityEnded());

    public static Activity Start(string description) =>
        new(ActivityId.CreateUnique(), new[] { new ActivityStarted(description) });

    [UsedImplicitly]
    internal void Apply(ActivityStarted @event)
    {
        Description = @event.Description;
        State = ActivityState.Running;
    }

    [UsedImplicitly]
    internal void Apply(ActivityDescriptionChanged @event) =>
        Description = @event.Description;

    [UsedImplicitly]
    internal void Apply(ActivityPaused _) =>
        State = ActivityState.Paused;

    [UsedImplicitly]
    internal void Apply(ActivityResumed _) =>
        State = ActivityState.Running;

    [UsedImplicitly]
    internal void Apply(ActivityEnded _) =>
        State = ActivityState.Ended;
}
