using System.Linq.Expressions;
using MediatR;
using MongoDB.Driver;
using Tracky.Domain.Common;
using Tracky.ReadModels.Common;
using Tracky.ReadModels.Persistence;

namespace Tracky.Infrastructure.Repositories;

public sealed class MongoDbRepository<T>(IMongoDatabase database) : IRepository<T> where T : IReadModel
{
    private static string CollectionName => typeof(T).Name;

    public async Task<Result<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> predicate) =>
        (await database.GetCollection<T>(CollectionName).FindAsync(predicate))
        .ToEnumerable()
        .ToResult();

    public async Task<Result<T>> GetByIdAsync(Guid id) =>
        await database
            .GetCollection<T>(CollectionName)
            .Find(x => x.Id == id.ToString())
            .SingleAsync();

    public async Task<Result<Unit>> InsertAsync(T entity)
    {
        await database
            .GetCollection<T>(CollectionName)
            .InsertOneAsync(entity);

        return Unit.Value;
    }

    public async Task<Result<Unit>> UpdateAsync(T entity)
    {
        await database
            .GetCollection<T>(CollectionName)
            .ReplaceOneAsync(x => x.Id == entity.Id, entity);

        return Unit.Value;
    }
}
