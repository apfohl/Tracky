using Ardalis.SmartEnum;

namespace Tracky.Domain.Activity.Enums;

public sealed class ActivityState : SmartEnum<ActivityState>
{
    public static readonly ActivityState Created = new(nameof(Created), 1);
    public static readonly ActivityState Running = new(nameof(Running), 2);
    public static readonly ActivityState Paused = new(nameof(Paused), 3);
    public static readonly ActivityState Ended = new(nameof(Ended), 4);

    private ActivityState(string name, int value) : base(name, value)
    {
    }
}
