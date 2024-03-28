using MediatR;

namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId> : Entity<TId> where TId : AggregateRootId
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

    public Task<Result<Unit>> Commit(Func<TId, int, IEnumerable<DomainEvent>, Task<Result<Unit>>> persist) =>
        persist(Id, version, uncommittedEvents.AsReadOnly())
            .TapAsync(_ =>
            {
                version += uncommittedEvents.Count;
                uncommittedEvents.Clear();
            });

    protected Result<Unit> ApplyDomainEvent(DomainEvent domainEvent) =>
        ((Result<Unit>)((dynamic)this).Apply(domainEvent))
        .Tap(_ => uncommittedEvents.Add(domainEvent));
}
