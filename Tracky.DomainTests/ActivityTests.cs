using Tracky.Domain.Activity;
using Tracky.Domain.Activity.Enums;
using Tracky.Domain.Activity.Errors;
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
        var activity = Activity.Create();

        activity.Start(description).Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Description.Should().Be(description);
        activity.State.Should().Be(ActivityState.Running);

        return activity.Commit((id, version, events) =>
        {
            id.Should().Be(activity.Id);
            version.Should().Be(0);

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
        var events = new List<DomainEvent<ActivityId>>
        {
            new ActivityStarted(id, "Test Description"),
            new ActivityDescriptionChanged(id, "New Description")
        };

        var activity = new Activity(id, events);

        activity.Id.Should().Be(id);
        activity.Description.Should().Be("New Description");
        activity.State.Should().Be(ActivityState.Running);

        return activity.Commit((i, v, ev) =>
        {
            i.Should().Be(activity.Id);
            v.Should().Be(2);

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
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.ChangeDescription("New Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Description.Should().Be("New Description");

        return activity.Commit((id, version, events) =>
        {
            id.Should().Be(activity.Id);
            version.Should().Be(0);

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
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Pause().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.State.Should().Be(ActivityState.Paused);
    }

    [Test]
    public static void Resume_attaches_event_for_resuming_activity()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Pause().Switch(_ => { }, error => Assert.Fail(error.ToString()));
        activity.Resume().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.State.Should().Be(ActivityState.Running);
    }

    [Test]
    public static void End_attaches_event_for_ending_activity()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.End().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.State.Should().Be(ActivityState.Ended);
    }

    [Test]
    public static void Resume_returns_error_when_activity_has_already_ended()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));
        activity.End().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Resume().Switch(
            _ => Assert.Fail("Activity should not be resumed!"),
            error => error.Should().BeOfType<ActivityAlreadyEnded>());
    }

    [Test]
    public static void Pause_returns_error_when_activity_has_already_ended()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));
        activity.End().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Pause().Switch(
            _ => Assert.Fail("Activity should not be paused!"),
            error => error.Should().BeOfType<ActivityAlreadyEnded>());
    }
}
