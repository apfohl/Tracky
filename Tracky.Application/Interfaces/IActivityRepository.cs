using MediatR;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Application.Interfaces;

public interface IActivityRepository
{
    Task<Result<Unit>> AddAsync(Activity activity);
    Task<Result<IEnumerable<Activity>>> ListAsync();
}
