using Tracky.Application.Activities.ReadModels;
using Tracky.Application.Common;

namespace Tracky.Application.Activities.Queries.ShowActivity;

public sealed record ShowActivityQuery(Guid Id) : IQuery<ActivityReadModel>;
