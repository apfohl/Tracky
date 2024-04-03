using MediatR;
using Tracky.Application.Activities.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed record ListActivitiesQuery : IRequest<Result<IEnumerable<ActivityReadModel>>>;
