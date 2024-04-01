using MediatR;

namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId> : Entity<TId> where TId : AggregateRootId
{
    private readonly List<DomainEvent> uncommittedEvents = [];
    private long version;

    protected AggregateRoot(TId id, IEnumerable<DomainEvent> events)
    {
        Id = id;
        foreach (var @event in events)
        {
            ApplyDomainEvent(@event);
            version++;
        }
    }

    public Task<Result<Unit>> Commit(Func<long, IEnumerable<DomainEvent>, Task<Result<long>>> persist) =>
        persist(version, uncommittedEvents.AsReadOnly())
            .TapAsync(newVersion =>
            {
                version = newVersion;
                uncommittedEvents.Clear();
            })
            .MapAsync(_ => Unit.Value);

    protected Result<Unit> ApplyDomainEvent(DomainEvent domainEvent) =>
        ((Result<Unit>)((dynamic)this).Apply((dynamic)domainEvent))
        .Tap(_ => uncommittedEvents.Add(domainEvent));
}
