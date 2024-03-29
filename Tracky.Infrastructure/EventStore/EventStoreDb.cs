using System.Text.Json;
using EventStore.Client;
using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.EventStore;

public sealed class EventStoreDb(EventStoreClient eventStoreClient) : IEventStore
{
    public Task<Result<IEnumerable<DomainEvent>>> ReadEventsAsync<TAggregateId>(TAggregateId aggregateId)
        where TAggregateId : AggregateRootId
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Unit>> AppendEventAsync<TAggregateId>(TAggregateId id, int version, DomainEvent @event)
        where TAggregateId : AggregateRootId
    {
        var eventData = new EventData(
            Uuid.NewUuid(),
            @event.GetType().Name,
            SerializeEvent(@event)
        );

        await eventStoreClient.AppendToStreamAsync(
            id.Value.ToString(),
            version == 0 ? StreamRevision.None : StreamRevision.FromInt64(version),
            new[] { eventData }
        );

        return Unit.Value;
    }

    private static byte[] SerializeEvent(DomainEvent @event) =>
        JsonSerializer.SerializeToUtf8Bytes(@event);

    private static DomainEvent DeserializeEvent(ReadOnlyMemory<byte> data) =>
        JsonSerializer.Deserialize<DomainEvent>(data.Span) ?? throw new InvalidOperationException();
}
