using Tracky.ReadModels.Common;

namespace Tracky.ReadModels.Activities;

public enum ActivityState
{
    Running = 1,
    Paused,
    Ended
}

public sealed record ActivityReadModel(string Id, ActivityState State, string Description) : IReadModel;
