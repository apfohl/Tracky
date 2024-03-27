using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityDescriptionChanged(ActivityId ActivityId, string Description)
    : DomainEvent<ActivityId>(ActivityId);
