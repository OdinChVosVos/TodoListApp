using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models;

public class ItemCategory
{
    [Key]
    public long Id { get; init; }
    
    [Column("title")]
    public string Title { get; set; }
    
    
    public ICollection<TodoItem>? TodoItems { get; set; }
}
