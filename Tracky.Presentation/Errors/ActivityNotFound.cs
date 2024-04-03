using Tracky.Domain.Common;

namespace Tracky.Presentation.Errors;

public sealed record ActivityNotFound(Guid Id) : Error;
