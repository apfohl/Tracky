using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Errors;

public sealed record ActivityIsNotRunning(ActivityState State) : Error;
