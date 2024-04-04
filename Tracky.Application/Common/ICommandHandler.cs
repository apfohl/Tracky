using MediatR;
using Tracky.Domain.Common;

namespace Tracky.Application.Common;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result<Unit>> where TCommand : ICommand;

public interface ICommandHandler<in TCommand, T> : IRequestHandler<TCommand, Result<T>> where TCommand : ICommand<T>;
