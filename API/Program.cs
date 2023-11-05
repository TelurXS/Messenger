using Application.Common.Extensions;
using Application.Common.Interfaces.Services;
using Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastrucutre(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/account/create", async (
    [FromBody] CreateAccount.Request request,
    [FromServices] IMediator mediator
    ) =>
{
    var result = await mediator.Send(request);

    return result.Match(
        account => Results.Ok(account),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapGet("account/{id}", async (
    [FromRoute] int id,
    [FromServices] IMediator mediator) => 
{
    var request = new GetAccountById.Request
    {
        Id = id,
    };

    var result = await mediator.Send(request);

    return result.Match(
        account => Results.Ok(account),
        notFound => Results.NotFound());
});

app.MapPut("account/{id}", async (
    [FromRoute] int id,
    [FromBody] UpdateAccount.Request request,
    [FromServices] IMediator mediator) =>
{
    request.Id = id;

    var result = await mediator.Send(request);

    return result.Match(
        account => Results.Ok(account),
        notFound => Results.NotFound(),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.Run();
