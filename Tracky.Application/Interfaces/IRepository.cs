using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Interfaces;

public interface IRepository<TAggregate, in TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : AggregateRootId
{
    Task<Result<TAggregate>> GetByIdAsync(TAggregateId id);

    Task<Result<Unit>> SaveAsync(TAggregate aggregate);
}
