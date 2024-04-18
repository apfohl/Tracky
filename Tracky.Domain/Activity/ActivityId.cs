using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record ActivityId : Identity
{
    private ActivityId(Guid Value) : base(Value)
    {
    }

    public static ActivityId CreateUnique() => new(Guid.NewGuid());

    public static ActivityId Create(Guid value) => new(value);

    public override string AsString() => $"{GetType().Name.Replace("Id", "")}_{Value}";
}
