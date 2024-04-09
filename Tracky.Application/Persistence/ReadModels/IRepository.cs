using System.Linq.Expressions;
using Tracky.Application.Common;
using Tracky.Domain.Common;

namespace Tracky.Application.Persistence.ReadModels;

public interface IRepository<T> where T : IReadModel
{
    Task<Result<IEnumerable<T>>> All(Expression<Func<T, bool>> predicate);

    Task<Result<T>> Lookup(Guid id);

    void Insert(T entity);

    void Update(T entity);

    void Delete(Guid id);
}
