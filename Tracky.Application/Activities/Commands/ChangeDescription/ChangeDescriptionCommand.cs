using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.ChangeDescription;

public sealed record ChangeDescriptionCommand(Guid Id, string Description) : IRequest<Result<Unit>>;
