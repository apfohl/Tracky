using MediatR;

namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId> : Entity<TId> where TId : AggregateRootId
{
    public const long InitialVersion = -1;

    private readonly List<DomainEvent> uncommittedEvents = [];
    private long version = InitialVersion;

    protected AggregateRoot(TId id, IEnumerable<DomainEvent> events)
    {
        Id = id;
        foreach (var @event in events)
        {
            ((dynamic)this).Apply((dynamic)@event);
            version++;
        }
    }

    public Task<Result<TId>> Commit(Func<long, IEnumerable<DomainEvent>, Task<Result<long>>> persist) =>
        persist(version, uncommittedEvents.AsReadOnly())
            .TapAsync(newVersion =>
            {
                version = newVersion;
                uncommittedEvents.Clear();
            })
            .MapAsync(_ => Id);

    protected Result<Unit> ApplyDomainEvent(DomainEvent domainEvent) =>
        ((Result<Unit>)((dynamic)this).Apply((dynamic)domainEvent))
        .Tap(_ => uncommittedEvents.Add(domainEvent));
}
