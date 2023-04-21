namespace CSharp.GraphQL;

public record EditTodoItemModel
(
    Guid Id,
    [Required, MinLength(1), MaxLength(255)] string Title,
    [MinLength(1), MaxLength(255)] string Description,
    PriorityLevel PriorityLevel
);
public partial class Mutation
{
    public async Task<TodoItem?> EditTodoItem(
        [Service] ITodoItemService service,
        EditTodoItemModel model,
        CancellationToken token = default
    )
    {
        var item = await service.FindByIdAsync(model.Id, token);
        if (item == null)
            return null;

        item.Title = model.Title;
        item.Description = model.Description;
        item.Priority = model.PriorityLevel;
        var result = await service.UpdateAsync(item, token);
        return result;
    }
}