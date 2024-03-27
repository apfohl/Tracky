using MediatR;
using Tracky.Domain.Common;
using Tracky.ReadModels.Activities;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed record ListActivitiesQuery : IRequest<Result<IEnumerable<ActivityReadModel>>>;
