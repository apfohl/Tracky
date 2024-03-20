namespace Tracky.Domain.Common;

public abstract record Entity<TId, TIdType> where TId : EntityId<TIdType>
{
    private readonly List<DomainEvent> uncommittedEvents = [];

    public TId Id { get; protected init; }

    protected IEnumerable<DomainEvent> UncommittedEvents => uncommittedEvents.AsReadOnly();

    protected void ClearDomainEvents() => uncommittedEvents.Clear();

    protected void ApplyDomainEvent(DomainEvent domainEvent)
    {
        ((dynamic)this).Apply((dynamic)domainEvent);

        uncommittedEvents.Add(domainEvent);
    }
}
