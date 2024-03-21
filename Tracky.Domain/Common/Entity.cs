namespace Tracky.Domain.Common;

public abstract record Entity<TId> where TId : ValueObject
{
    public TId Id { get; protected init; }
}
