using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.ValueObjects;

public sealed record Description(string Value) : ValueObject
{
    public static Description Create(string value) => new(value);
}
