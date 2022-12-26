using Microsoft.EntityFrameworkCore;

class TodoController
{
    public static async Task<IResult> GetAllTodos(TodoDB db)
    {
        return TypedResults.Ok(await db.Todos.ToArrayAsync());
    }
    public static async Task<IResult> GetCompleteTodos(TodoDB db)
    {
        return TypedResults.Ok(await db.Todos.Where(t => t.IsCompleted).ToListAsync());
    }
    public static async Task<IResult> GetTodo(int id, TodoDB db)
    {
        return await db.Todos.FindAsync(id)
            is Todo todo
                ? TypedResults.Ok(todo)
                : TypedResults.NotFound();
    }
    public static async Task<IResult> CreateTodo(Todo todo, TodoDB db)
    {
        db.Todos.Add(todo);
        await db.SaveChangesAsync();

        return TypedResults.Created($"/todoitems/{todo.Id}", todo);
    }
    public static async Task<IResult> UpdateTodo(int id, Todo inputTodo, TodoDB db)
    {
        var todo = await db.Todos.FindAsync(id);

        if (todo is null) return TypedResults.NotFound();

        todo.Name = inputTodo.Name;
        todo.IsCompleted = inputTodo.IsCompleted;

        await db.SaveChangesAsync();
        var response = new JsonReesponse
        {
            Status = StatusCodes.Status200OK,
            Message = "Updated successfully",
            Object = todo
        };
        return TypedResults.Json(response);
    }

    public static async Task<IResult> DeleteTodo(int id, TodoDB db)
    {
        if (await db.Todos.FindAsync(id) is Todo todo)
        {
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return TypedResults.Ok(todo);
        }

        return TypedResults.NotFound();
    }

    public class JsonReesponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Object Object { get; set; }
    }
}
