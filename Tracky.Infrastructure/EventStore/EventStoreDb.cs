using System.Text.Json;
using EventStore.Client;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.EventStore;

public sealed class EventStoreDb(EventStoreClient eventStoreClient) : IEventStore
{
    public Task<Result<IEnumerable<DomainEvent>>> ReadEventsAsync<TAggregateId>(TAggregateId aggregateId)
        where TAggregateId : AggregateRootId
    {
        throw new NotImplementedException();
    }

    public async Task<Result<long>> AppendEventsAsync<TAggregateId>(TAggregateId id, long version,
        IEnumerable<DomainEvent> events) where TAggregateId : AggregateRootId
    {
        try
        {
            var result = await eventStoreClient.AppendToStreamAsync(
                id.Value.ToString(),
                version == 0 ? StreamRevision.None : StreamRevision.FromInt64(version),
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

    private static DomainEvent DeserializeEvent(ReadOnlyMemory<byte> data) =>
        JsonSerializer.Deserialize<DomainEvent>(data.Span) ?? throw new InvalidOperationException();
}
