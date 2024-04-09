using MediatR;
using Tracky.Application.Common;
using Tracky.Application.Persistence.ReadModels;
using Tracky.Domain.Common;

namespace Tracky.Application.Persistence;

public interface IUnitOfWork<TReadModel> where TReadModel : IReadModel
{
    IRepository<TReadModel> Repository { get; }
    
    Task<Result<Unit>> SaveChanges();
}
