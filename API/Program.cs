using API.Middlewares;
using Application.Common.Extensions;
using Application.Features.Accounts;
using Application.Features.Groups;
using Application.Features.Messages;
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

app.MapGet("account/all", async (
    [FromServices] IMediator mediator) =>
{
    var request = new GetAllAccounts.Request();

    var result = await mediator.Send(request);

    return result.Match(
        account => Results.Ok(account),
        notFound => Results.NotFound());
});

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
    [FromServices] IMediator mediator) =>
{
    var request = new DeleteAccountById.Request
    {
        Id = id,
    };
    
    var result = await mediator.Send(request);

    return result.Match(
        success => Results.Ok(),
        notFound => Results.NotFound(),
        failed => Results.BadRequest());
});



app.MapGet("group/{id:int}", async (
    [FromRoute] int id,
    [FromServices] IMediator mediator) => 
{
    var request = new GetGroupById.Request
    {
        Id = id,
    };

    var result = await mediator.Send(request);

    return result.Match(
        group => Results.Ok(group),
        notFound => Results.NotFound());
});

app.MapGet("group/all", async (
    [FromServices] IMediator mediator) =>
{
    var request = new GetAllGroups.Request();

    var result = await mediator.Send(request);

    return result.Match(
        group => Results.Ok(group),
        notFound => Results.NotFound());
});

app.MapGet("group/{id:int}/accounts", async (
    [FromRoute] int id,
    [FromServices] IMediator mediator) =>
{
    var request = new GetAllAccountsFromGroup.Request
    {
        GroupId = id,
    };

    var result = await mediator.Send(request);

    return result.Match(
        accounts => Results.Ok(accounts),
        notFound => Results.NotFound());
});

app.MapPost("/group", async (
    [FromBody] CreateGroup.Request request,
    [FromServices] IMediator mediator
) =>
{
    var result = await mediator.Send(request);

    return result.Match(
        group => Results.Ok(group),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapPost("/group/{id:int}/add-account", async (
    [FromRoute] int id,
    [FromBody] AddAccountToGroup.Request request,
    [FromServices] IMediator mediator
) =>
{
    request.GroupId = id;
    
    var result = await mediator.Send(request);

    return result.Match(
        group => Results.Ok(group),
        notFound => Results.NotFound(),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapPost("/group/{id:int}/remove-account", async (
    [FromRoute] int id,
    [FromBody] RemoveAccountFromGroup.Request request,
    [FromServices] IMediator mediator
) =>
{
    request.GroupId = id;
    
    var result = await mediator.Send(request);

    return result.Match(
        group => Results.Ok(group),
        notFound => Results.NotFound(),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapPut("group/{id:int}", async (
    [FromRoute] int id,
    [FromBody] UpdateGroup.Request request,
    [FromServices] IMediator mediator) =>
{
    request.Id = id;

    var result = await mediator.Send(request);

    return result.Match(
        group => Results.Ok(group),
        notFound => Results.NotFound(),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapDelete("group/{id:int}", async (
    [FromRoute] int id,
    [FromServices] IMediator mediator) =>
{
    var request = new DeleteGroupById.Request
    {
        Id = id,
    };
    
    var result = await mediator.Send(request);

    return result.Match(
        success => Results.Ok(),
        notFound => Results.NotFound(),
        failed => Results.BadRequest());
});



app.MapGet("message/{id:int}", async (
    [FromRoute] int id,
    [FromServices] IMediator mediator) => 
{
    var request = new GetMessageById.Request
    {
        Id = id,
    };

    var result = await mediator.Send(request);

    return result.Match(
        message => Results.Ok(message),
        notFound => Results.NotFound());
});

app.MapPost("/message", async (
    [FromBody] CreateMessageAtGroupFromAccount.Request request,
    [FromServices] IMediator mediator
) =>
{
    var result = await mediator.Send(request);

    return result.Match(
        message => Results.Ok(message),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapPut("message/{id:int}", async (
    [FromRoute] int id,
    [FromBody] UpdateMessage.Request request,
    [FromServices] IMediator mediator) =>
{
    request.Id = id;

    var result = await mediator.Send(request);

    return result.Match(
        message => Results.Ok(message),
        notFound => Results.NotFound(),
        validationFailed => Results.BadRequest(validationFailed.Errors),
        failed => Results.BadRequest());
});

app.MapDelete("message/{id:int}", async (
    [FromRoute] int id,
    [FromServices] IMediator mediator) =>
{
    var request = new DeleteMessageById.Request
    {
        Id = id,
    };
    
    var result = await mediator.Send(request);

    return result.Match(
        success => Results.Ok(),
        notFound => Results.NotFound(),
        failed => Results.BadRequest());
});

app.Run();
