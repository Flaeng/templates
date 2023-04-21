namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public class UpdateTodoItemRequest : CreateTodoItemRequest
{
    public Guid Id { get; set; }
}
