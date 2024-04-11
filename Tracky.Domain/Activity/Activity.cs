using JetBrains.Annotations;
using MediatR;
using OneOf;
using OneOf.Types;
using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Errors;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.Domain.Activity;

public sealed record Activity : AggregateRoot<ActivityId>
{
    private readonly List<Pause> pauses = [];
    private OneOf<DateTime, None> CurrentPauseStart { get; set; } = new None();

    public string Description { get; private set; }
    public ActivityState State { get; private set; } = ActivityState.Created;

    public IEnumerable<Pause> Pauses => pauses;

    public Activity(ActivityId id, IEnumerable<DomainEvent> events) : base(id, events)
    {
    }

    public static Activity Create() =>
        new(ActivityId.CreateUnique(), Array.Empty<DomainEvent>());

    public Common.Result<Activity> Start(string description) =>
        ApplyDomainEvent(new ActivityStarted(description)).Map(_ => this);

    public Common.Result<Activity> ChangeDescription(string description) =>
        ApplyDomainEvent(new ActivityDescriptionChanged(description)).Map(_ => this);

    public Common.Result<Activity> Pause() => ApplyDomainEvent(new ActivityPaused(DateTime.Now)).Map(_ => this);

    public Common.Result<Activity> Resume() => ApplyDomainEvent(new ActivityResumed(DateTime.Now)).Map(_ => this);

    public Common.Result<Activity> End() => ApplyDomainEvent(new ActivityEnded()).Map(_ => this);

    [UsedImplicitly]
    internal Common.Result<Unit> Apply(ActivityStarted @event)
    {
        switch (State)
        {
            case ActivityState.Running:
                return new ActivityAlreadyStarted();
            case ActivityState.Ended:
                return new ActivityAlreadyEnded();
            case ActivityState.Paused:
                return new ActivityAlreadyStarted();
            case ActivityState.Created:
                Description = @event.Description;
                State = ActivityState.Running;
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }

    [UsedImplicitly]
    internal Common.Result<Unit> Apply(ActivityDescriptionChanged @event)
    {
        Description = @event.Description;
        return Unit.Value;
    }

    [UsedImplicitly]
    internal Common.Result<Unit> Apply(ActivityPaused @event)
    {
        switch (State)
        {
            case ActivityState.Running:
                State = ActivityState.Paused;
                CurrentPauseStart = @event.PausedAt;
                return Unit.Value;
            case ActivityState.Ended:
                return new ActivityAlreadyEnded();
            case ActivityState.Paused:
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }

    [UsedImplicitly]
    internal Common.Result<Unit> Apply(ActivityResumed @event)
    {
        switch (State)
        {
            case ActivityState.Running:
                return Unit.Value;
            case ActivityState.Ended:
                return new ActivityAlreadyEnded();
            case ActivityState.Paused:
                State = ActivityState.Running;
                pauses.Add(new Pause(CurrentPauseStart.AsT0, @event.ResumedAt));
                CurrentPauseStart = new None();
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }

    [UsedImplicitly]
    internal Common.Result<Unit> Apply(ActivityEnded _)
    {
        switch (State)
        {
            case ActivityState.Running:
                State = ActivityState.Ended;
                return Unit.Value;
            case ActivityState.Ended:
                return Unit.Value;
            case ActivityState.Paused:
                State = ActivityState.Ended;
                return Unit.Value;
            default:
                throw new ArgumentException(nameof(State));
        }
    }
}
