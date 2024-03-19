using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId, Guid>
{
    public Description Description { get; private set; }

    private Activity(ActivityId id) : base(id)
    {
    }

    public static Activity Create(Description description)
    {
        var activity = new Activity(ActivityId.CreateUnique());

        activity.ApplyDomainEvent(new ActivityCreated(description));

        return activity;
    }

    internal void Apply(ActivityCreated @event) =>
        Description = @event.Description;

    internal void Apply(ActivityDescriptionUpdated @event) =>
        Description = @event.Description;
}
