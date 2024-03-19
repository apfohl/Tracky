using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public record ActivityDescriptionUpdated(Description Description) : DomainEvent;
