using System.Linq.Expressions;
using MediatR;
using Tracky.Application.Common;
using Tracky.Domain.Common;

namespace Tracky.Application.Persistence.ReadModels;

public interface IRepository<T> where T : IReadModel
{
    Task<Result<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> predicate);

    Task<Result<T>> GetByIdAsync(Guid id);

    Task<Result<Unit>> InsertAsync(T entity);

    Task<Result<Unit>> UpdateAsync(T entity);
}
