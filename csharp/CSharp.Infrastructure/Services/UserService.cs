namespace CSharp.Infrastructure.Services;

public interface IUserService : IBaseService<User>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken token = default);
    Task<User?> FindByUsernameAsync(string username, CancellationToken token = default);
}
public class UserService : BaseService<User>, IUserService
{
    public UserService(ApplicationDataContext dataContext)
        : base(dataContext)
    {
    }

    public User? FindByEmail(string email)
    {
        return base.Query()
            .Where(x => x.Email == email)
            .SingleOrDefault();
    }

    public async Task<User?> FindByEmailAsync(string email, CancellationToken token = default)
    {
        return await base.Query()
            .Where(x => x.Email == email)
            .SingleOrDefaultAsync(token);
    }

    public User? FindByUsername(string username)
    {
        return base.Query()
            .Where(x => x.Username == username)
            .SingleOrDefault();
    }

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken token = default)
    {
        return await base.Query()
            .Where(x => x.Username == username)
            .SingleOrDefaultAsync(token);
    }
}