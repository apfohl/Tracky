using Tracky.Application.Common;
using Tracky.Domain.Activity;

namespace Tracky.Application.Activities.Commands.StartActivity;

public sealed record StartActivityCommand(string Description) : ICommand<ActivityId>;
