using MediatR;

namespace Tracky.Domain.Common;

public abstract record DomainEvent<TAggregateId>(TAggregateId ActivityId) : INotification
    where TAggregateId : AggregateRootId
{
    public TAggregateId ActivityId { get; protected init; } = ActivityId;

    public DateTime OccurredOn { get; } = DateTime.Now;
}
