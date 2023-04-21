namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public class CreateTodoListRequest
{
    [Required, MinLength(1), MaxLength(255)]
    public string? Title { get; set; }
};
