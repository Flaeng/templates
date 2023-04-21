namespace CSharp.GraphQL;

public record MarkTodoItemCompletionModel
(
    Guid Id,
    bool isCompleted
);
public partial class Mutation
{
    public async Task<TodoItem?> MarkTodoItemCompletion(
        [Service] ITodoItemService service,
        MarkTodoItemCompletionModel model,
        CancellationToken token = default
    )
    {
        var item = await service.FindByIdAsync(model.Id, token);
        if (item == null)
            return null;

        item.IsCompleted = model.isCompleted;
        var result = await service.UpdateAsync(item, token);
        return result;
    }
}