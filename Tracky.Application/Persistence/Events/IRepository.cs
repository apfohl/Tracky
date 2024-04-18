using Tracky.Domain.Common;

namespace Tracky.Application.Persistence.Events;

public interface IRepository<TAggregate, TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : Identity
{
    Task<Result<TAggregate>> GetByIdAsync(TAggregateId id);

    Task<Result<TAggregateId>> SaveAsync(TAggregate aggregate);
}
