namespace CSharp.Infrastructure.Services;

public interface IBaseService<T>
    where T : class
{
    IServiceContext Context { get; }
    IQueryable<T> Query();
    // void Create(T item);
    Task<T> CreateAsync(T item, CancellationToken token = default);
    // T? FindById(Guid id);
    ValueTask<T?> FindByIdAsync(Guid id, CancellationToken token = default);
    // void Update(T entity);
    Task<T> UpdateAsync(T entity, CancellationToken token = default);
    // void Delete(T item);
    // void Delete(Guid id);
    Task DeleteAsync(Guid id, CancellationToken token = default);
}
public abstract class BaseService<T> : IBaseService<T>
    where T : class
{
    public IServiceContext Context { get; }

    protected DbContext dataContext { get; }
    protected DbSet<T> set { get; }

    public BaseService(ApplicationDataContext dataContext)
    {
        this.dataContext = dataContext;
        this.Context = new ServiceContext(dataContext);
        this.set = this.dataContext.Set<T>();
    }

    public virtual void Create(T item)
    {
        set.Add(item);
        dataContext.SaveChanges();
    }

    public virtual async Task<T> CreateAsync(T item, CancellationToken token = default)
    {
        set.Add(item);
        await dataContext.SaveChangesAsync(token);
        return item;
    }

    public virtual IQueryable<T> Query()
    {
        return set;
    }

    public virtual T? FindById(Guid id)
    {
        return set.Find(id);
    }

    public virtual ValueTask<T?> FindByIdAsync(Guid id, CancellationToken token = default)
    {
        return set.FindAsync(id, token);
    }

    public virtual void Delete(T item)
    {
        set.Remove(item);
        dataContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var item = FindById(id);
        if (item != null)
            set.Remove(item);
        dataContext.SaveChanges();
    }

    public async Task DeleteAsync(Guid id, CancellationToken token = default)
    {
        var item = await FindByIdAsync(id, token);
        if (item != null)
            set.Remove(item);
        await dataContext.SaveChangesAsync(token);
    }

    public void Update(T entity)
    {
        dataContext.Entry(entity).State = EntityState.Modified;
        dataContext.SaveChanges();
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken token = default)
    {
        dataContext.Entry(entity).State = EntityState.Modified;
        await dataContext.SaveChangesAsync(token);
        return entity;
    }
}