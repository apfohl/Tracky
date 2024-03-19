using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityCreated(Activity Activity) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.Now;
}
