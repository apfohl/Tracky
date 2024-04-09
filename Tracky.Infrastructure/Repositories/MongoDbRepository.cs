using System.Linq.Expressions;
using MongoDB.Driver;
using Tracky.Application.Common;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;
using Tracky.Infrastructure.UnitOfWork;

namespace Tracky.Infrastructure.Repositories;

public sealed class MongoDbRepository<T>(MongoDbContext<T> dbContext) : IRepository<T>
    where T : IReadModel
{
    public async Task<Result<IEnumerable<T>>> All(Expression<Func<T, bool>> predicate) =>
        (await dbContext.Collection.FindAsync(predicate))
        .ToEnumerable()
        .ToResult();

    public async Task<Result<T>> Lookup(Guid id) =>
        await dbContext.Collection.Find(model => model.Id == id.ToString()).SingleAsync();

    public void Insert(T entity) =>
        dbContext.Insert(entity);

    public void Update(T entity) =>
        dbContext.Update(entity);

    public void Delete(Guid id) =>
        dbContext.Delete(id);
}
