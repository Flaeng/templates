using CSharp.Domain.Entities;

namespace CSharp.Domain.QueryableExtensions;

public static class TodoListExtensions
{
    public static IQueryable<TodoList> WhereOwnerIs(this IQueryable<TodoList> query, Guid userId)
    {
        return query.Where(x => x.OwnerId == userId);
    }
}
