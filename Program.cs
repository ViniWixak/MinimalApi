using MinimalApi.Data;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();
var app = builder.Build();

app.MapGet("/", () => Results.Ok(new Todo(Guid.NewGuid(), "Ir a academia", false)));

app.Run();
