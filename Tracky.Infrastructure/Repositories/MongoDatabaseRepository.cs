using System.Linq.Expressions;
using Tracky.ReadModels.Common;
using Tracky.ReadModels.Persistence;

namespace Tracky.Infrastructure.Repositories;

public sealed class MongoDatabaseRepository<T> : IRepository<T> where T : IReadModel
{
    public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
