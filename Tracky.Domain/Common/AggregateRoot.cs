using MediatR;

namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    private readonly List<DomainEvent> uncommittedEvents = [];
    private int version;

    protected AggregateRoot(TId id, IEnumerable<DomainEvent> events)
    {
        Id = id;
        foreach (var @event in events)
        {
            ApplyDomainEvent(@event);
            version++;
        }
    }

    public async Task Commit(Func<TId, int, IEnumerable<DomainEvent>, Task> persist)
    {
        await persist(Id, version, uncommittedEvents.AsReadOnly());
        version += uncommittedEvents.Count;
        uncommittedEvents.Clear();
    }

    protected Result<Unit> ApplyDomainEvent(DomainEvent domainEvent)
    {
        Result<Unit> result = ((dynamic)this).Apply((dynamic)domainEvent);

        result.Switch(
            _ => uncommittedEvents.Add(domainEvent),
            _ => { });

        return result;
    }
}
