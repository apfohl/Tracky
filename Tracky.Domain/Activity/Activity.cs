using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId, Guid>
{
    private Activity(ActivityId id) : base(id)
    {
    }

    public static Activity Create()
    {
        var activity = new Activity(ActivityId.CreateUnique());

        activity.AddDomainEvent(new ActivityCreated(activity));

        return activity;
    }
}
