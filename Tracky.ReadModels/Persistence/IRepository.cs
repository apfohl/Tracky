using System.Linq.Expressions;
using MediatR;
using Tracky.Domain.Common;
using Tracky.ReadModels.Common;

namespace Tracky.ReadModels.Persistence;

public interface IRepository<T> where T : IReadModel
{
    Task<Result<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> predicate);

    Task<Result<T>> GetByIdAsync(Guid id);

    Task<Result<Unit>> InsertAsync(T entity);

    Task<Result<Unit>> UpdateAsync(T entity);
}
