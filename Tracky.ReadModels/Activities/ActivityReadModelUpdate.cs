using MediatR;
using Tracky.Domain.Common;

namespace Tracky.ReadModels.Activities;

public sealed record ActivityReadModelUpdate(Guid Id, IEnumerable<DomainEvent> Events) : INotification;
