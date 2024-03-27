using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityEnded(ActivityId ActivityId) : DomainEvent<ActivityId>(ActivityId);
