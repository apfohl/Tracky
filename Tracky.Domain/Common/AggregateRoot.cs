namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId, TIdType> : Entity<TId> where TId : AggregateRootId<TIdType>
{
    public new AggregateRootId<TIdType> Id { get; protected init; }

    protected AggregateRoot(TId id) => Id = id;
}
