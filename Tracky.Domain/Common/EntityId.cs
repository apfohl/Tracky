namespace Tracky.Domain.Common;

public abstract record EntityId<TId>(TId Value) : ValueObject;
