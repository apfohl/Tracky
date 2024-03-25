using MediatR;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed record ListActivitiesQuery : IRequest<Result<IEnumerable<Activity>>>;
