namespace CSharp.WebApi.Models;

public class TodoItemModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public PriorityLevel Priority { get; set; }
}