using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ToDoList.Models;

public class TodoItem
{
    [Key]
    public long Id { get; init; }
    
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("completed")]
    public bool IsComplete { get; set; }
    
    
    [Column("category")]
    [ForeignKey("Category")]
    public long? CategoryId { get; set; }
    
    
    public ItemCategory? Category { get; set; }
}
