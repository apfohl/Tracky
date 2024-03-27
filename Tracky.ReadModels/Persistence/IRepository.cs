using System.Linq.Expressions;
using Tracky.ReadModels.Common;

namespace Tracky.ReadModels.Persistence;

public interface IRepository<T> where T : IReadModel
{
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

    Task<T> GetByIdAsync(string id);

    Task InsertAsync(T entity);

    Task UpdateAsync(T entity);
}
