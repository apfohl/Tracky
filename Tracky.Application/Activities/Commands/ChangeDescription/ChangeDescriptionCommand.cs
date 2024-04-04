using Tracky.Application.Common;

namespace Tracky.Application.Activities.Commands.ChangeDescription;

public sealed record ChangeDescriptionCommand(Guid Id, string Description) : ICommand;
