using CSharp.GraphQL.Models;

namespace CSharp.GraphQL.Mapping;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<TodoList, TodoListModel>()
            .ReverseMap();

        CreateMap<CreateTodoItemModel, TodoListModel>();
    }
}