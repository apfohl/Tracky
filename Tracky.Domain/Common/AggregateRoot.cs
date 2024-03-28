using MediatR;

namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId> : Entity<TId> where TId : AggregateRootId
{
    private readonly List<DomainEvent<TId>> uncommittedEvents = [];
    private int version;

    protected AggregateRoot(TId id, IEnumerable<DomainEvent<TId>> events)
    {
        Id = id;
        foreach (var @event in events)
        {
            ApplyDomainEvent(@event);
            version++;
        }
    }

    public async Task<Result<Unit>> Commit(Func<TId, int, IEnumerable<DomainEvent<TId>>, Task<Result<Unit>>> persist)
    {
        var result = await persist(Id, version, uncommittedEvents.AsReadOnly());

        result.Switch(_ =>
            {
                version += uncommittedEvents.Count;
                uncommittedEvents.Clear();
            },
            _ => { });

        return result;
    }

    protected Result<Unit> ApplyDomainEvent(DomainEvent<TId> domainEvent)
    {
        Result<Unit> result = ((dynamic)this).Apply((dynamic)domainEvent);

        result.Switch(
            _ => uncommittedEvents.Add(domainEvent),
            _ => { });

        return result;
    }
}
