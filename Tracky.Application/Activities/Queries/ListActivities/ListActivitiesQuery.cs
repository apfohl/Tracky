using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Common;

namespace Tracky.Application.Activities.Queries.ListActivities;

public sealed record ListActivitiesQuery : IQuery<IEnumerable<ActivityReadModel>>;
