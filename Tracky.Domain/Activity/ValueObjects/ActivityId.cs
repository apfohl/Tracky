using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.ValueObjects;

public sealed record ActivityId(Guid Value) : EntityId<Guid>(Value)
{
    public static ActivityId CreateUnique() => new(Guid.NewGuid());

    public static ActivityId Create(Guid value) => new(value);
}
