using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data;

public class TodoContext : IdentityDbContext
{
    
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<ItemCategory> ItemCategory { get; set; }
    
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}