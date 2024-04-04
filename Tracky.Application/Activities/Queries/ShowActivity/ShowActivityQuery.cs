using MediatR;
using Tracky.Application.Activities.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.Queries.ShowActivity;

public sealed record ShowActivityQuery(Guid Id) : IRequest<Result<ActivityReadModel>>;
