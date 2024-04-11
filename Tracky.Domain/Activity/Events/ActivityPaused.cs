using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityPaused(DateTime PausedAt) : DomainEvent;
