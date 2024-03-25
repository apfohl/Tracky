using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.EventStore;

public interface IEventStore
{
    Task<Result<IEnumerable<DomainEvent<TAggregateId, TAggregateIdType>>>>
        ReadEventsAsync<TAggregateId, TAggregateIdType>(TAggregateId aggregateId)
        where TAggregateId : AggregateRootId<TAggregateIdType>;

    Task<Result<Unit>> AppendEventAsync<TAggregateId, TAggregateIdType>(
        DomainEvent<TAggregateId, TAggregateIdType> @event) where TAggregateId : AggregateRootId<TAggregateIdType>;
}
