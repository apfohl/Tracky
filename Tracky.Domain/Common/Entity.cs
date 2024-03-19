namespace Tracky.Domain.Common;

public abstract record Entity<TId, TIdType> where TId : EntityId<TIdType>
{
    private readonly List<IDomainEvent> uncommittedEvents = [];

    public TId Id { get; protected init; }

    public IEnumerable<IDomainEvent> UncommittedEvents => uncommittedEvents.AsReadOnly();

    public void ClearDomainEvents() => uncommittedEvents.Clear();

    public void ApplyDomainEvent(IDomainEvent domainEvent)
    {
        ((dynamic)this).Apply((dynamic)domainEvent);

        uncommittedEvents.Add(domainEvent);
    }
}
