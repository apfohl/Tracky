using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityStarted(ActivityId ActivityId, string Description)
    : DomainEvent<ActivityId, Guid>(ActivityId);
