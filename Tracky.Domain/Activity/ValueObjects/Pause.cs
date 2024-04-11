using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.ValueObjects;

public sealed record Pause(DateTime Start, DateTime End) : ValueObject
{
    TimeSpan Duration => End - Start;
}
