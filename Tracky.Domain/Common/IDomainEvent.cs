using MediatR;

namespace Tracky.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
