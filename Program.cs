using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDB>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var todoItems = app.MapGroup("/todoitems");
todoItems.MapGet("/", TodoController.GetAllTodos);
todoItems.MapGet("/complete", TodoController.GetCompleteTodos);
todoItems.MapGet("/{id}", TodoController.GetTodo);
todoItems.MapPost("/", TodoController.CreateTodo);
todoItems.MapPut("/{id}", TodoController.UpdateTodo);
todoItems.MapDelete("/{id}", TodoController.DeleteTodo);

// #region 
// app.MapGet("/todoitems", async (TodoDB db) =>
// await db.Todos.ToListAsync());
// app.MapGet("/todoitems/complete", async (TodoDB db) =>
//     await db.Todos.Where(t => t.IsCompleted).ToListAsync());
// app.MapGet("/todoitems/{id}", async (int id, TodoDB db) =>
//     await db.Todos.FindAsync(id)
//         is Todo todo
//             ? Results.Ok(todo)
//             : Results.NotFound());
// app.MapPost("/todoitems", async (Todo todo, TodoDB db) =>
// {
//     db.Todos.Add(todo);
//     await db.SaveChangesAsync();

//     return Results.Created($"/todoitems/{todo.Id}", todo);
// });
// app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDB db) =>
// {
//     var todo = await db.Todos.FindAsync(id);

//     if (todo is null) return Results.NotFound();

//     todo.Name = inputTodo.Name;
//     todo.IsCompleted = inputTodo.IsCompleted;

//     await db.SaveChangesAsync();
//     return Results.Json(todo);

//     // return Results.NoContent();
// });
// app.MapDelete("/todoitems/{id}", async (int id, TodoDB db) =>
// {
//     if (await db.Todos.FindAsync(id) is Todo todo)
//     {
//         db.Todos.Remove(todo);
//         await db.SaveChangesAsync();
//         return Results.Ok(todo);
//     }

//     return Results.NotFound();
// });
// #engregion

app.MapGet("/", () => "Hello World!");

app.Run();
