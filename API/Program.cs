using API.Middlewares;
using Application.Common.Extensions;
using Application.Features.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName!
        .Split('.')
        .Last()
        .Replace("+", ""));
});

builder.Services.AddApplication();
builder.Services.AddInfrastrucutre(builder.Configuration);

builder.Services.AddSingleton<ExceptionMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.MapPost("/account", async (
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

app.MapGet("account/{id:int}", async (
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

app.MapPut("account/{id:int}", async (
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

app.MapDelete("account/{id:int}", async (
    [FromRoute] int id,
    [FromBody] DeleteAccountById.Request request,
    [FromServices] IMediator mediator) =>
{
    var result = await mediator.Send(request);

    return result.Match(
        success => Results.Ok(),
        notFound => Results.NotFound(),
        failed => Results.BadRequest());
});

app.Run();
