using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Common;
using Tracky.Infrastructure.EventStore;
using Tracky.ReadModels.Activities;

namespace Tracky.Infrastructure.Repositories;

public sealed class EventStoreDbRepository<TAggregate, TAggregateId>(IEventStore eventStore, IPublisher publisher)
    : IRepository<TAggregate, TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : AggregateRootId
{
    public async Task<Result<TAggregate>> GetByIdAsync(TAggregateId id) =>
        (await eventStore.ReadEventsAsync(id))
        .Map(events => MaterializeAggregate(id, events));

    public async Task<Result<TAggregateId>> SaveAsync(TAggregate aggregate) =>
        await aggregate.Commit(async (version, events) =>
        {
            var eventList = events.ToList();

            var newVersion = await eventStore.AppendEventsAsync(aggregate.Id, version, eventList);

            await publisher.Publish(new ActivityReadModelUpdate(aggregate.Id.Value, eventList));

            return newVersion;
        });

    private static TAggregate MaterializeAggregate(TAggregateId id, IEnumerable<DomainEvent> events) =>
        (TAggregate)Activator.CreateInstance(typeof(TAggregate), id, events);
}
