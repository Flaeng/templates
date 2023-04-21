using CSharp.Domain.Enums;

namespace CSharp.Domain.Entities;

public class TodoItem : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public PriorityLevel Priority { get; set; }
    public bool IsCompleted { get; set; }

    public Guid ListId { get; set; }
    public TodoList? List { get; set; }
}