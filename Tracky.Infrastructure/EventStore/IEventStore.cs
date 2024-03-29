using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.EventStore;

public interface IEventStore
{
    Task<Result<IEnumerable<DomainEvent>>> ReadEventsAsync<TAggregateId>(TAggregateId aggregateId)
        where TAggregateId : AggregateRootId;

    Task<Result<Unit>> AppendEventAsync<TAggregateId>(TAggregateId id, int version, DomainEvent @event)
        where TAggregateId : AggregateRootId;
}
