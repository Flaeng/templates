namespace CSharp.GraphQL;

public partial class Query
{
    // [UsePaging, UseProjection, UseFiltering, UseSorting]
    [UsePaging, UseFiltering, UseSorting]
    public IQueryable<TodoItem>? TodoItems(
        [Service] ITodoItemService service,
        [Service] IHttpContextAccessor httpContextAccessor
    )
    {
        var hasUserId = httpContextAccessor.TryGetRequesterUserId(out var userId);
        if (hasUserId != true)
            return null;

        return service.Query()
            .Where(x => x.List.Owner.Id == userId);
    }
}