namespace CSharp.GraphQL.Models;

public class TodoListModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string Title { get; set; } = "";

    public ICollection<TodoItemModel> Items { get; set; } = new List<TodoItemModel>();
}