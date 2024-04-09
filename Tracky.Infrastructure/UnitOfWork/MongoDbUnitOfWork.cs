using MediatR;
using Tracky.Application.Common;
using Tracky.Application.Persistence;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;
using Tracky.Infrastructure.Repositories;

namespace Tracky.Infrastructure.UnitOfWork;

public sealed class MongoDbUnitOfWork<TReadModel> : IUnitOfWork<TReadModel>
    where TReadModel : IReadModel
{
    private readonly Lazy<IRepository<TReadModel>> repository;
    private readonly MongoDbContext<TReadModel> context;

    public MongoDbUnitOfWork(MongoDbContext<TReadModel> context)
    {
        this.context = context;
        repository = new Lazy<IRepository<TReadModel>>(() => new MongoDbRepository<TReadModel>(this.context));
    }

    public IRepository<TReadModel> Repository =>
        repository.Value;

    public async Task<Result<Unit>> SaveChanges() =>
        await context.Commit();
}
