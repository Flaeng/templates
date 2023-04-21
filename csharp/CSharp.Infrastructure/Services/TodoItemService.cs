namespace CSharp.Infrastructure.Services;

public interface ITodoItemService : IBaseService<TodoItem>
{
    bool SetCompletionAsync(Guid id, bool completed, CancellationToken token = default);
}
public class TodoItemService : BaseService<TodoItem>, ITodoItemService
{
    public TodoItemService(ApplicationDataContext dataContext)
        : base(dataContext)
    {
    }

    public bool SetCompletionAsync(Guid id, bool completed, CancellationToken token = default)
    {
        var entity = FindById(id);
        if (entity == null)
            return false;

        entity.IsCompleted = completed;
        return true;
    }
}