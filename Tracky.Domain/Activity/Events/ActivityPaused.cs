using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityPaused(ActivityId ActivityId) : DomainEvent<ActivityId>(ActivityId);
