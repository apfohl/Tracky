using Ardalis.SmartEnum;

namespace Tracky.Domain.Activity.Enums;

public sealed class ActivityState : SmartEnum<ActivityState>
{
    public static readonly ActivityState Started = new(nameof(Started), 1);
    public static readonly ActivityState Paused = new(nameof(Paused), 2);
    public static readonly ActivityState Ended = new(nameof(Ended), 3);

    private ActivityState(string name, int value) : base(name, value)
    {
    }
}
