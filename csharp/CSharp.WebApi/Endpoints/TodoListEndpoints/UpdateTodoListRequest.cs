namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public class UpdateTodoListRequest : CreateTodoListRequest
{
    public Guid Id { get; set; }
}
