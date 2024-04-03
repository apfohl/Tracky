using Tracky.Domain.Common;

namespace Tracky.Application.Interfaces;

public interface IRepository<TAggregate, TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : AggregateRootId
{
    Task<Result<TAggregate>> GetByIdAsync(TAggregateId id);

    Task<Result<TAggregateId>> SaveAsync(TAggregate aggregate);
}
