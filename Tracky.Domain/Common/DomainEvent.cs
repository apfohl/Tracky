using MediatR;

namespace Tracky.Domain.Common;

public abstract record DomainEvent : INotification
{
    public DateTime OccurredOn { get; } = DateTime.Now;
}
