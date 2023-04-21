namespace CSharp.GraphQL;

public record CreateTodoItemModel
(
    Guid ListId,
    [Required, MinLength(1), MaxLength(255)] string Title,
    [MinLength(1), MaxLength(255)] string Description,
    PriorityLevel PriorityLevel
);
public partial class Mutation
{
    public async Task<TodoItem?> CreateTodoItem(
        [Service] ITodoListService listService,
        [Service] ITodoItemService itemService,
        CreateTodoItemModel model,
        CancellationToken token = default
    )
    {
        var list = await listService.FindByIdAsync(model.ListId);
        if (list == null)
            return null;

        var result = await itemService.CreateAsync(new TodoItem
        {
            Title = model.Title,
            Description = model.Description,
            Priority = model.PriorityLevel,
            List = list,
        }, token);
        return result;
    }
}