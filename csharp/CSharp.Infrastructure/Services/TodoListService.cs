namespace CSharp.Infrastructure.Services;

public interface ITodoListService : IBaseService<TodoList> { }
public class TodoListService : BaseService<TodoList>, ITodoListService
{
    public TodoListService(ApplicationDataContext dataContext)
        : base(dataContext)
    {
    }
}