namespace Tracky.Domain.Common;

public abstract record AggregateRootId(Guid Value) : EntityId<Guid>(Value);
