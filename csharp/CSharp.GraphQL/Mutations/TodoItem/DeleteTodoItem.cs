namespace CSharp.GraphQL;

public record DeleteTodoItemModel
(
    Guid Id
);
public partial class Mutation
{
    public async Task DeleteTodoItem(
        [Service] ITodoItemService service,
        DeleteTodoItemModel model,
        CancellationToken token = default
    )
    {
        await service.DeleteAsync(model.Id, token);
    }
}