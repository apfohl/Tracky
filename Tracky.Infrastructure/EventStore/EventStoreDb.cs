using System.Text.Json;
using EventStore.Client;
using Tracky.Domain.Common;
using Tracky.Infrastructure.Errors;

namespace Tracky.Infrastructure.EventStore;

public sealed class EventStoreDb(EventStoreClient eventStoreClient) : IEventStore
{
    public Task<Result<IEnumerable<DomainEvent>>> ReadEventsAsync<TAggregateId>(TAggregateId aggregateId)
        where TAggregateId : Identity =>
        eventStoreClient
            .ReadStreamAsync(Direction.Forwards, aggregateId.AsString(), StreamPosition.Start)
            .Select(@event => DeserializeEvent(@event.Event.Data, @event.Event.EventType))
            .AggregateAsync(
                (Result<List<DomainEvent>>)new List<DomainEvent>(),
                (result, @event) => @event.Bind(domainEvent => result.Tap(list => list.Add(domainEvent))))
            .AsTask()
            .MapAsync(list => list.AsEnumerable());

    public async Task<Result<long>> AppendEventsAsync<TAggregateId>(TAggregateId id, long version,
        IEnumerable<DomainEvent> events) where TAggregateId : Identity
    {
        try
        {
            var result = await eventStoreClient.AppendToStreamAsync(
                id.AsString(),
                version == AggregateRoot<TAggregateId>.InitialVersion
                    ? StreamRevision.None
                    : StreamRevision.FromInt64(version),
                events.Select(@event => new EventData(Uuid.NewUuid(), @event.GetType().Name, SerializeEvent(@event)))
                    .ToArray());

            return result.NextExpectedStreamRevision.ToInt64();
        }
        catch (WrongExpectedVersionException e)
        {
            return new Errors.WrongExpectedVersion(e);
        }
    }

    private static byte[] SerializeEvent(DomainEvent @event) =>
        JsonSerializer.SerializeToUtf8Bytes(@event);

    private static Result<Type> DomainEventType(string eventType) =>
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrError(type => type.Name == eventType, new DomainEventTypeNotFound());

    private static Result<DomainEvent> DeserializeEvent(ReadOnlyMemory<byte> data, string eventType) =>
        DomainEventType(eventType)
            .Map(type => (DomainEvent)JsonSerializer.Deserialize(data.Span, type));
}
