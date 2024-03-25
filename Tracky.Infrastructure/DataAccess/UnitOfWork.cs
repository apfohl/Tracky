using Tracky.Application.Interfaces;

namespace Tracky.Infrastructure.DataAccess;

public sealed class UnitOfWork : IUnitOfWork
{
    public IActivityRepository Activities { get; }
}
