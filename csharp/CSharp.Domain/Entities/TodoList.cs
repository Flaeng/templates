namespace CSharp.Domain.Entities;

public class TodoList : BaseEntity
{
    public string Title { get; set; } = "";
    public List<TodoItem> Items { get; set; } = new();

    public Guid OwnerId { get; set; }
    public User? Owner { get; set; }
}