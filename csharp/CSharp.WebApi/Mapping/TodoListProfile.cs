using CSharp.WebApi.Endpoints.TodoListEndpoints;

namespace CSharp.WebApi.Mapping;

public class TodoListProfile : Profile
{
    public TodoListProfile()
    {
        CreateMap<TodoList, TodoListModel>().ReverseMap();

        CreateMap<CreateTodoListRequest, TodoListModel>().ReverseMap();
        CreateMap<CreateTodoListRequest, TodoList>().ReverseMap();
        CreateMap<UpdateTodoListRequest, TodoListModel>().ReverseMap();
        CreateMap<UpdateTodoListRequest, TodoList>().ReverseMap();
    }
}