using System.Linq.Expressions;
using MediatR;
using Tracky.Domain.Common;
using Tracky.ReadModels.Common;
using Tracky.ReadModels.Persistence;

namespace Tracky.Infrastructure.Repositories;

public sealed class MongoDatabaseRepository<T> : IRepository<T> where T : IReadModel
{
    public Task<Result<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Result<T>> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Unit>> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
