using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Common;

public interface IQuery<T> : IRequest<Result<T>>;
