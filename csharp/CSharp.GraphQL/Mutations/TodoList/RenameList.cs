namespace CSharp.GraphQL;

public record RenameListModel
(
    Guid Id,
    [Required, MinLength(1), MaxLength(255)] string Title
);
public partial class Mutation
{
    public async Task<TodoList?> RenameList(
        [Service] ITodoListService service,
        RenameListModel model,
        CancellationToken token = default
    )
    {
        var item = await service.FindByIdAsync(model.Id, token);
        if (item == null)
            return null;

        item.Title = model.Title;
        var result = await service.UpdateAsync(item, token);
        return result;
    }
}