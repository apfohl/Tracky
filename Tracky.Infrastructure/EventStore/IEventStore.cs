using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.EventStore;

public interface IEventStore
{
    Task<Result<IEnumerable<DomainEvent<TAggregateId>>>> ReadEventsAsync<TAggregateId>(TAggregateId aggregateId)
        where TAggregateId : AggregateRootId;

    Task<Result<Unit>> AppendEventAsync<TAggregateId>(DomainEvent<TAggregateId> @event)
        where TAggregateId : AggregateRootId;
}
