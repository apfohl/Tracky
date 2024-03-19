namespace Tracky.Domain.Common;

public abstract record Entity<TId> where TId : ValueObject
{
    private readonly List<IDomainEvent> uncommittedEvents = [];

    public TId Id { get; protected init; }

    public IReadOnlyList<IDomainEvent> UncommittedEvents => uncommittedEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) => uncommittedEvents.Add(domainEvent);

    public void ClearDomainEvents() => uncommittedEvents.Clear();
}
