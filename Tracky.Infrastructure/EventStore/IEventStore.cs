using Tracky.Domain.Common;

namespace Tracky.Infrastructure.EventStore;

public interface IEventStore
{
    Task<Result<IEnumerable<DomainEvent>>> ReadEventsAsync<TAggregateId>(TAggregateId aggregateId)
        where TAggregateId : Identity;

    Task<Result<long>> AppendEventsAsync<TAggregateId>(TAggregateId id, long version, IEnumerable<DomainEvent> events)
        where TAggregateId : Identity;
}
