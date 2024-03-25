using MediatR;
using Tracky.Application.Interfaces;
using Tracky.Domain.Activity;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.Repositories;

public sealed class ActivityRepository : IActivityRepository
{
    public Task<Result<Unit>> AddAsync(Activity activity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Activity>>> ListAsync()
    {
        throw new NotImplementedException();
    }
}
