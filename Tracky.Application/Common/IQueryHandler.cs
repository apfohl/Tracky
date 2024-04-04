using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Common;

public interface IQueryHandler<in TQuery, T> : IRequestHandler<TQuery, Result<T>>
    where TQuery : IQuery<T>;
