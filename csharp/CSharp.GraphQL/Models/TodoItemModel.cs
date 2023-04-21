namespace CSharp.GraphQL.Models;

public class TodoItemModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public PriorityLevel Priority { get; set; }
}