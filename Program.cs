using MinimalApi.Data;
using MinimalApi.ViewModels;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/v1/todos", (DataContext context) =>
{
    var todos = context.Todos;
    return todos is not null ? Results.Ok(todos) : Results.NotFound();
});

app.MapPost("/v1/todos", (DataContext context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});

app.MapPut("/v1/todos", (DataContext context, UpdateTodoViewModel model) => {
    model.MapTo();
    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    var todo = context.Todos.First( t => t.Id == model.TodoId);
    if (todo is not null)
    {
        todo.Title = model.Title;        
        context.SaveChanges();
    }
    return Results.Ok(todo);
});

app.Run();
