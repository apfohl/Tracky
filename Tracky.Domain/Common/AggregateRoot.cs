namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId, TIdType> : Entity<TId, TIdType> where TId : EntityId<TIdType>
{
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
        await persist(Id, version, UncommittedEvents);
        version += UncommittedEvents.Count();
        ClearDomainEvents();
    }
}
