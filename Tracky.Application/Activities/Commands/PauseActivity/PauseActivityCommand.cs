using Tracky.Application.Common;

namespace Tracky.Application.Activities.Commands.PauseActivity;

public sealed record PauseActivityCommand(Guid Id) : ICommand;
