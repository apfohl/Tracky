using EventStore.Client;
using Tracky.Domain.Common;

namespace Tracky.Infrastructure.Errors;

public sealed record WrongExpectedVersion(WrongExpectedVersionException Exception) : Error;
