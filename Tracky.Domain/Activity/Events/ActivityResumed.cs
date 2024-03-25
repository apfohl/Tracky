using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityResumed(ActivityId ActivityId) : DomainEvent;
