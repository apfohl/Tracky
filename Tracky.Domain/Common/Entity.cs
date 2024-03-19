namespace Tracky.Domain.Common;

public abstract record Entity<TId, TIdType> where TId : EntityId<TIdType>
{
    private readonly List<DomainEvent> uncommittedEvents = [];

    public TId Id { get; protected init; }

    public IEnumerable<DomainEvent> UncommittedEvents => uncommittedEvents.AsReadOnly();

    public void ClearDomainEvents() => uncommittedEvents.Clear();

    public void ApplyDomainEvent(DomainEvent domainEvent)
    {
        ((dynamic)this).Apply((dynamic)domainEvent);

        uncommittedEvents.Add(domainEvent);
    }
}
