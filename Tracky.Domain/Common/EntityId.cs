namespace Tracky.Domain.Common;

public abstract record EntityId<TId>(TId Value) : ValueObject
{
    public static implicit operator TId(EntityId<TId> id) => id.Value;
}
