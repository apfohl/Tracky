namespace Tracky.Domain.Common;

public abstract record AggregateRootId<TId>(TId Value) : EntityId<TId>(Value);
