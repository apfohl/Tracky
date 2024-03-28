using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Common;
using Tracky.Infrastructure.EventStore;

namespace Tracky.Infrastructure.Repositories;

public sealed class EventStoreRepository<TAggregate, TAggregateId>(IEventStore eventStore)
    : IRepository<TAggregate, TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : AggregateRootId
{
    public async Task<Result<TAggregate>> GetByIdAsync(TAggregateId id) =>
        (await eventStore.ReadEventsAsync(id))
        .Map(events => MaterializeAggregate(id, events));

    public async Task<Result<Unit>> SaveAsync(TAggregate aggregate) =>
        await aggregate.Commit(async (id, version, events) =>
        {
            Result<Unit> result = Unit.Value;

            foreach (var @event in events)
            {
                result = await result.BindAsync(_ => eventStore.AppendEventAsync(@event));
            }

            return result;
        });

    private static TAggregate MaterializeAggregate(TAggregateId id, IEnumerable<DomainEvent<TAggregateId>> events) =>
        (TAggregate)Activator.CreateInstance(typeof(TAggregate), id, events);
}
