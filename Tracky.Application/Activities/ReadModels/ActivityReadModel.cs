using Tracky.Application.Common;

namespace Tracky.Application.Activities.ReadModels;

public enum ActivityState
{
    Running = 1,
    Paused,
    Ended
}

public sealed record ActivityReadModel(string Id, ActivityState State, string Description) : IReadModel;
