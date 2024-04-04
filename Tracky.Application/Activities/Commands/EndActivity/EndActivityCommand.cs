using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.EndActivity;

public sealed record EndActivityCommand(Guid Id) : IRequest<Result<Unit>>;
