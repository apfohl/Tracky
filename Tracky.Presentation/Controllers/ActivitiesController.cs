using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tracky.Application.Activities.Commands.StartActivity;
using Tracky.Application.Activities.Queries.ListActivities;
using Tracky.Application.Activities.ReadModels;
using Tracky.Domain.Common;

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
}

public sealed record ActivityNotFound(Guid Id) : Error;
