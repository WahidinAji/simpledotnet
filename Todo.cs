
using Microsoft.EntityFrameworkCore;

class Todo
{
    public int Id {get; set; }
    public string? Name {get; set; }
    public bool IsCompleted {get; set; }
}

class TodoDB : DbContext 
{
    public TodoDB(DbContextOptions<TodoDB> options) : base(options){}
    public DbSet<Todo> Todos => Set<Todo>();
}