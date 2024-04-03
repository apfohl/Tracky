using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tracky.Application.Activities.Commands.StartActivity;
using Tracky.Application.Activities.Queries.ListActivities;
using Tracky.Domain.Common;
using Tracky.ReadModels.Activities;

namespace Tracky.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ActivitiesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityReadModel>>> GetActivities() =>
        (await sender.Send(new ListActivitiesQuery()))
        .Match<ActionResult>(Ok, NotFound);

    [HttpPost]
    public async Task<ActionResult<Guid>> StartActivity([FromBody] string description) =>
        (await sender.Send(new StartActivityCommand(description)))
        .Match<ActionResult>(
            activityId => CreatedAtAction(nameof(GetActivity), new { id = activityId.Value }),
            _ => StatusCode(500));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActivityReadModel>> GetActivity(Guid id) =>
        (await sender.Send(new ListActivitiesQuery()))
        .Bind(activities => activities.FirstOrError(activity => activity.Id == id.ToString(), new ActivityNotFound(id)))
        .Match<ActionResult>(Ok, NotFound);

    // [HttpPut("{id:guid}")]
    // public IActionResult PutActivity(Guid id, Activity activity)
    // {
    //     if (id != activity.Id)
    //     {
    //         return BadRequest();
    //     }
    //
    //     var existingActivity = Activities.Find(a => a.Id == id);
    //     if (existingActivity == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     existingActivity =
    //         activity; // In a real application, you would handle updating the properties of the existing activity.
    //
    //     return NoContent();
    // }
    //
    //
    // [HttpDelete("{id:guid}")]
    // public IActionResult DeleteActivity(Guid id)
    // {
    //     var activity = Activities.Find(a => a.Id == id);
    //     if (activity == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     Activities.Remove(activity);
    //
    //     return NoContent();
    // }
}

public sealed record ActivityNotFound(Guid Id) : Error;
