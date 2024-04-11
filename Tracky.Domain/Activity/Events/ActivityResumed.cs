using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityResumed(DateTime ResumedAt) : DomainEvent;
