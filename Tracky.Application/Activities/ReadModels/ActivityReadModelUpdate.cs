using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Activities.ReadModels;

public sealed record ActivityReadModelUpdate(Guid Id, IEnumerable<DomainEvent> Events) : INotification;
