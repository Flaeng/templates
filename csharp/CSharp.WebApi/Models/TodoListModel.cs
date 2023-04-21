namespace CSharp.WebApi.Models;

public class TodoListModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public string Title { get; set; } = "";

    public ICollection<TodoItemModel> Items { get; set; } = new List<TodoItemModel>();
}