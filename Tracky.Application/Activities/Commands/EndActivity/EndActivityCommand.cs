using Tracky.Application.Common;

namespace Tracky.Application.Activities.Commands.EndActivity;

public sealed record EndActivityCommand(Guid Id) : ICommand;
