using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.PauseActivity;

public sealed record PauseActivityCommand(Guid Id) : IRequest<Result<Unit>>;
