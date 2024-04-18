using Tracky.Domain.Activity;
using Tracky.Domain.Activity.Errors;
using Tracky.Domain.Activity.Events;
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

        return activity.Commit((version, events) =>
        {
            version.Should().Be(-1);

            var eventsList = events.ToList();
            eventsList.Should().HaveCount(1);
            eventsList.Should().ContainSingle(e => e is ActivityStarted);

            return Task.FromResult<Result<long>>(1);
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

        var activity = new Activity(id, events);

        activity.Id.Should().Be(id);

        return activity.Commit((v, ev) =>
        {
            v.Should().Be(1);

            var eventsList = ev.ToList();
            eventsList.Should().HaveCount(0);

            return Task.FromResult<Result<long>>(4);
        });
    }

    [Test]
    public static Task ChangeDescription_attaches_event_for_changing_description()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.ChangeDescription("New Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        return activity.Commit((version, events) =>
        {
            version.Should().Be(-1);

            var eventsList = events.ToList();
            eventsList.Should().HaveCount(2);
            eventsList.Should().ContainSingle(e => e is ActivityStarted);
            eventsList.Should().ContainSingle(e => e is ActivityDescriptionChanged);

            return Task.FromResult<Result<long>>(2);
        });
    }

    [Test]
    public static void Pause_attaches_event_for_pausing_activity()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Pause().Switch(_ => Assert.Pass(), error => Assert.Fail(error.ToString()));
    }

    [Test]
    public static void Resume_attaches_event_for_resuming_activity()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Pause().Switch(_ => Assert.Pass(), error => Assert.Fail(error.ToString()));
        activity.Resume().Switch(_ => Assert.Pass(), error => Assert.Fail(error.ToString()));
    }

    [Test]
    public static void End_attaches_event_for_ending_activity()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.End().Switch(_ => Assert.Pass(), error => Assert.Fail(error.ToString()));
    }

    [Test]
    public static void Resume_returns_error_when_activity_is_not_paused()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));
        activity.End().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Resume().Switch(
            _ => Assert.Fail("Activity should not be resumed!"),
            error => error.Should().BeOfType<ActivityIsNotPaused>());
    }

    [Test]
    public static void Pause_returns_error_when_activity_is_not_running()
    {
        var activity = Activity.Create();
        activity.Start("Test Description").Switch(_ => { }, error => Assert.Fail(error.ToString()));
        activity.End().Switch(_ => { }, error => Assert.Fail(error.ToString()));

        activity.Pause().Switch(
            _ => Assert.Fail("Activity should not be paused!"),
            error => error.Should().BeOfType<ActivityIsNotRunning>());
    }
}
