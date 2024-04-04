using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Common;

public interface IBaseCommand;

public interface ICommand : IRequest<Result<Unit>>, IBaseCommand;

public interface ICommand<T> : IRequest<Result<T>>, IBaseCommand;
