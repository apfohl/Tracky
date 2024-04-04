using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.ResumeActivity;

public sealed record ResumeActivityCommand(Guid Id) : IRequest<Result<Unit>>;
