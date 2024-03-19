namespace Tracky.Domain.Common;

public abstract record AggregateRoot<TId, TIdType> : Entity<TId, TIdType> where TId : EntityId<TIdType>
{
    protected AggregateRoot(TId id) => Id = id;
}
