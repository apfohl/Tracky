using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.Repositories;

public sealed class EventStoreRepository<TAggregate, TAggregateId> : IRepository<TAggregate, TAggregateId>
    where TAggregate : AggregateRoot<TAggregateId>
    where TAggregateId : AggregateRootId
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
