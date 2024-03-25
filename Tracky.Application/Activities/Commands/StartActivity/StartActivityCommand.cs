using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.StartActivity;

public sealed record StartActivityCommand(string Description) : IRequest<Result<Unit>>;
