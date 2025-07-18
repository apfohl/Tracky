using Tracky.Domain.Common;

namespace Tracky.Domain.Activity.Events;

public sealed record ActivityStarted(string Description) : DomainEvent;
