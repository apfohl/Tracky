using MediatR;
using MongoDB.Driver;
using Tracky.Application.Common;

namespace Tracky.Infrastructure.UnitOfWork;

public sealed class MongoDbContext<TReadModel>(IMongoClient client, IMongoDatabase database)
    where TReadModel : IReadModel
{
    private static string CollectionName => typeof(TReadModel).Name;

    private readonly List<Func<IClientSessionHandle, CancellationToken, Task>> actions = [];

    public readonly IMongoCollection<TReadModel> Collection =
        database.GetCollection<TReadModel>(CollectionName);

    public void Insert(TReadModel model) =>
        actions.Add((s, cancellationToken) =>
            Collection.InsertOneAsync(s, model, cancellationToken: cancellationToken));

    public void Update(TReadModel model) =>
        actions.Add((s, cancellationToken) =>
            Collection.ReplaceOneAsync(s, x => x.Id == model.Id, model, cancellationToken: cancellationToken));

    public void Delete(Guid id) =>
        actions.Add((s, cancellationToken) =>
            Collection.DeleteOneAsync(s, x => x.Id == id.ToString(), cancellationToken: cancellationToken));

    public async Task<Unit> Commit()
    {
        using var session = await client.StartSessionAsync();

        return await session.WithTransactionAsync(async (s, cancellationToken) =>
        {
            foreach (var action in actions)
            {
                await action(s, cancellationToken);
            }

            actions.Clear();

            return Unit.Value;
        });
    }
}
