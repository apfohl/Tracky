namespace Tracky.Application.Interfaces;

public interface IUnitOfWork
{
    IActivityRepository Activities { get; }
}
