using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.ValueObjects;

public sealed record ActivityId : AggregateRootId
{
    private ActivityId(Guid Value) : base(Value)
    {
    }

    public static ActivityId CreateUnique() => new(Guid.NewGuid());

    public static ActivityId Create(Guid value) => new(value);
}
