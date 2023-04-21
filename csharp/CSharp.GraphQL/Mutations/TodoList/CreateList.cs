namespace CSharp.GraphQL;

public record CreateListModel
(
    [Required, MinLength(1), MaxLength(255)] string Title
);
public partial class Mutation
{
    public async Task<TodoList?> CreateList(
        [Service] ITodoListService service,
        [Service] IUserService userService,
        [Service] IHttpContextAccessor httpContextAccessor,
        CreateListModel model,
        CancellationToken token = default
    )
    {
        var hasUserId = httpContextAccessor.TryGetRequesterUserId(out var userId);
        if (hasUserId != true)
            return null;

        var user = await userService.FindByIdAsync(userId, token);
        if (user == null)
            return null;

        var result = await service.CreateAsync(new TodoList
        {
            Title = model.Title,
            Owner = user
        });
        return result;
    }
}