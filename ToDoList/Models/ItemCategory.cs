using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models;

public class ItemCategory
{
    [Key]
    public long Id { get; init; }
    
    [Column("title")]
    public string Title { get; set; }
    
    
    public ICollection<TodoItem>? TodoItems { get; set; }
    
    
    
    [ForeignKey("User")]
    public string? UserId { get; set; }
    
    public IdentityUser? User { get; set; }
}
