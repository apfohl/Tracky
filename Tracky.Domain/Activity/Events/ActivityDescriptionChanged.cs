using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityDescriptionChanged(string Description) : DomainEvent;
