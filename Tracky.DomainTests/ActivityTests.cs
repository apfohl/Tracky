using Tracky.Domain.Activity;
using Tracky.Domain.Activity.Events;
using Tracky.Domain.Activity.ValueObjects;

namespace Tracky.DomainTests;

public static class ActivityTests
{
    [Test]
    public static void ActivityCreated_creates_activity()
    {
        var description = new Description("Test Description");
        var activity = Activity.Create(description);

        activity.Description.Should().Be(description);
        activity.UncommittedEvents.Should().ContainSingle(e => e is ActivityCreated);
    }

    [Test]
    public static void ApplyDomainEvent_applies_event()
    {
        var activity = Activity.Create(new Description("Test Description"));

        activity.ApplyDomainEvent(new ActivityDescriptionUpdated(new Description("New Description")));

        activity.UncommittedEvents.Should().HaveCount(2);
        activity.UncommittedEvents.Should().ContainSingle(e => e is ActivityCreated);
        activity.UncommittedEvents.Should().ContainSingle(e => e is ActivityDescriptionUpdated);
    }

    [Test]
    public static void ClearDomainEvents_clears_uncommitted_events()
    {
        var activity = Activity.Create(new Description("Test Description"));

        activity.ClearDomainEvents();

        activity.UncommittedEvents.Should().BeEmpty();
    }

    [Test]
    public static void ActivityDescriptionUpdated_updates_description()
    {
        var activity = Activity.Create(new Description("Test Description"));
        var newDescription = new Description("New Description");
        var activityDescriptionUpdated = new ActivityDescriptionUpdated(newDescription);

        activity.ClearDomainEvents();

        activity.ApplyDomainEvent(activityDescriptionUpdated);

        activity.Description.Should().Be(newDescription);
        activity.UncommittedEvents.Should().HaveCount(1);
        activity.UncommittedEvents.Should().ContainSingle(e => e is ActivityDescriptionUpdated);
    }
}
