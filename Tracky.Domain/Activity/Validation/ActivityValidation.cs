using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Errors;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Validation;

public static class ActivityValidation
{
    public static Result<ActivityState> ValidateNotRunning(ActivityState state) =>
        state switch
        {
            ActivityState.Running => new ActivityIsRunning(state),
            _ => state
        };

    public static Result<ActivityState> ValidateNotEnded(ActivityState state) =>
        state switch
        {
            ActivityState.Ended => new ActivityAlreadyEnded(state),
            _ => state
        };

    public static Result<ActivityState> ValidateRunning(ActivityState state) =>
        state switch
        {
            ActivityState.Running => state,
            _ => new ActivityIsNotRunning(state)
        };

    public static Result<ActivityState> ValidatePaused(ActivityState state) =>
        state switch
        {
            ActivityState.Paused => state,
            _ => new ActivityIsNotPaused(state)
        };
}
