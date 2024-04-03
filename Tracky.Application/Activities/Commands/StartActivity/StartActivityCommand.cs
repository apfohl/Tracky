using MediatR;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Commands.StartActivity;

public sealed record StartActivityCommand(string Description) : IRequest<Result<ActivityId>>;
