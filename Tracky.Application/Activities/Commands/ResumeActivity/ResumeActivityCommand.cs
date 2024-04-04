using Tracky.Application.Common;

namespace Tracky.Application.Activities.Commands.ResumeActivity;

public sealed record ResumeActivityCommand(Guid Id) : ICommand;
