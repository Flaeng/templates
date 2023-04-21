using CSharp.Domain.Entities;

namespace CSharp.Domain.QueryableExtensions;

public static class BaseEntityExtensions
{
    public static IQueryable<T> WhereIdIs<T>(this IQueryable<T> query, Guid id)
        where T : BaseEntity
    {
        return query.Where(x => x.Id == id);
    }
}