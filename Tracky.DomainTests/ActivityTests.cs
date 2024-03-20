using Tracky.Domain.Activity;
using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;
using Tracky.Domain.Common;

namespace Tracky.DomainTests;

public static class ActivityTests
{
    [Test]
    public static Task Start_creates_started_activity()
    {
        const string description = "Test Description";
        var activity = Activity.Start(description);

        activity.Description.Should().Be(description);
        activity.State.Should().Be(ActivityState.Started);

        return activity.Persist((id, events) =>
        {
            id.Should().Be(activity.Id);

            var eventsList = events.ToList();
            eventsList.Should().HaveCount(1);
            eventsList.Should().ContainSingle(e => e is ActivityStarted);

            return Task.CompletedTask;
        });
    }

    [Test]
    public static Task Materialize_creates_an_activity_from_an_event_list()
    {
        var id = ActivityId.CreateUnique();
        var events = new List<DomainEvent>
        {
            new ActivityStarted("Test Description"),
            new ActivityDescriptionChanged("New Description")
        };

        var activity = Activity.Materialize(id, events);

        activity.Id.Should().Be(id);
        activity.Description.Should().Be("New Description");
        activity.State.Should().Be(ActivityState.Started);

        return activity.Persist((i, ev) =>
        {
            i.Should().Be(activity.Id);

            var eventsList = ev.ToList();
            eventsList.Should().HaveCount(2);
            eventsList.Should().ContainSingle(e => e is ActivityStarted);
            eventsList.Should().ContainSingle(e => e is ActivityDescriptionChanged);

            return Task.CompletedTask;
        });
    }

    [Test]
    public static Task ChangeDescription_attaches_event_for_changing_description()
    {
        var activity = Activity.Start("Test Description");

        activity.ChangeDescription("New Description");

        activity.Description.Should().Be("New Description");

        return activity.Persist((id, events) =>
        {
            id.Should().Be(activity.Id);

            var eventsList = events.ToList();
            eventsList.Should().HaveCount(2);
            eventsList.Should().ContainSingle(e => e is ActivityStarted);
            eventsList.Should().ContainSingle(e => e is ActivityDescriptionChanged);

            return Task.CompletedTask;
        });
    }

    [Test]
    public static void Pause_attaches_event_for_pausing_activity()
    {
        var activity = Activity.Start("Test Description");

        activity.Pause();

        activity.State.Should().Be(ActivityState.Paused);
    }

    [Test]
    public static void End_attaches_event_for_ending_activity()
    {
        var activity = Activity.Start("Test Description");

        activity.End();

        activity.State.Should().Be(ActivityState.Ended);
    }
}
