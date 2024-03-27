using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.Repositories;

public sealed class EventStoreRepository<TAggregate, TAggregateId, TAggregateIdType> :
    IRepository<TAggregate, TAggregateId, TAggregateIdType>
    where TAggregate : AggregateRoot<TAggregateId, TAggregateIdType>
    where TAggregateId : AggregateRootId<TAggregateIdType>
{
    public Task<Result<TAggregate>> GetByIdAsync(TAggregateId id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> SaveAsync(TAggregate aggregate)
    {
        throw new NotImplementedException();
    }
}
