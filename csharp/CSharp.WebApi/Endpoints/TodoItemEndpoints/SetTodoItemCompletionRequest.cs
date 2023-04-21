namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public record SetTodoItemCompletionRequest(Guid Id, bool Completed);
