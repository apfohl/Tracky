using MediatR;

namespace Tracky.Domain.Common;

public abstract record DomainEvent<TAggregateId, TAggregateIdType>(TAggregateId ActivityId) : INotification
    where TAggregateId : AggregateRootId<TAggregateIdType>
{
    public TAggregateId ActivityId { get; protected init; } = ActivityId;

    public DateTime OccurredOn { get; } = DateTime.Now;
}
