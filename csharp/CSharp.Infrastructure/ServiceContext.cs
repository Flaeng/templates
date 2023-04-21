namespace CSharp.Infrastructure;

public interface IServiceContext
{
    ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
}
public class ServiceContext : IServiceContext
{
    protected readonly DbContext DbContext;

    public ServiceContext(DbContext dbContext)
    {
        this.DbContext = dbContext;
    }

    public async ValueTask SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}