using CSharp.Domain.Entities;

namespace CSharp.Domain.QueryableExtensions;

public static class TodoItemExtensions
{
    public static IQueryable<TodoItem> WhereOwnerIs(this IQueryable<TodoItem> query, Guid userId)
    {
        return query.Where(x => x.List != null && x.List.OwnerId == userId);
    }
    public static IQueryable<TodoItem> InList(this IQueryable<TodoItem> query, Guid listId)
    {
        return query.Where(x => x.ListId == listId);
    }
}
