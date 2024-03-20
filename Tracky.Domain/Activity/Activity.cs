using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId, Guid>
{
    public string Description { get; private set; }
    public ActivityState State { get; private set; }

    private Activity(ActivityId id, IEnumerable<DomainEvent> events) : base(id, events)
    {
    }

    public void ChangeDescription(string description) =>
        ApplyDomainEvent(new ActivityDescriptionChanged(description));

    public void Pause() =>
        ApplyDomainEvent(new ActivityPaused());

    public void End() =>
        ApplyDomainEvent(new ActivityEnded());

    public static Activity Materialize(ActivityId id, IEnumerable<DomainEvent> events) =>
        new(id, events);

    public static Activity Start(string description) =>
        new(ActivityId.CreateUnique(), new[] { new ActivityStarted(description) });

    internal void Apply(ActivityStarted @event)
    {
        Description = @event.Description;
        State = ActivityState.Started;
    }

    internal void Apply(ActivityDescriptionChanged @event) =>
        Description = @event.Description;

    internal void Apply(ActivityPaused _) =>
        State = ActivityState.Paused;

    internal void Apply(ActivityEnded _) =>
        State = ActivityState.Ended;
}
