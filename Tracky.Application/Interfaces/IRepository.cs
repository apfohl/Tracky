using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Interfaces;

public interface IRepository<TAggregate, in TAggregateId, TAggregateIdType>
    where TAggregate : AggregateRoot<TAggregateId, TAggregateIdType>
    where TAggregateId : AggregateRootId<TAggregateIdType>
{
    Task<Result<TAggregate>> GetByIdAsync(TAggregateId id);

    Task<Result<Unit>> SaveAsync(TAggregate aggregate);
}
