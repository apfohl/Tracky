using MediatR;
using Tracky.Application;
using Tracky.Application.Activities.Commands.PauseActivity;
using Tracky.Application.Activities.Commands.StartActivity;
using Tracky.Application.Activities.Queries.ListActivities;
using Tracky.Domain.Common;
using Tracky.Infrastructure;
using Tracky.Presentation;
using Tracky.Presentation.Errors;
using Tracky.Presentation.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapGet("/activities", async (ISender sender) =>
        (await sender.Send(new ListActivitiesQuery()))
        .Match(Results.Ok, Results.NotFound))
    .WithName("GetActivities");

app.MapGet("/activities/{id:guid}", async (ISender sender, Guid id) =>
        (await sender.Send(new ListActivitiesQuery()))
        .Bind(activities => activities.FirstOrError(activity => activity.Id == id.ToString(), new ActivityNotFound(id)))
        .Match(Results.Ok, Results.NotFound))
    .WithName("GetActivityById");

app.MapPost("/activities", async (ISender sender, StartActivityRequestData requestData) =>
        (await sender.Send(new StartActivityCommand(requestData.Description)))
        .Match(
            activityId => Results.CreatedAtRoute("GetActivityById", new { id = activityId.Value }, activityId.Value),
            _ => Results.StatusCode(500)))
    .WithName("ListActivities");

app.MapPut("/activities/{id:guid}/pause", async (ISender sender, Guid id) =>
        (await sender.Send(new PauseActivityCommand(id)))
        .Match(_ => Results.Ok(), Results.NotFound))
    .WithName("PauseActivity");

app.Run();
