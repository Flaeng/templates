using CSharp.WebApi.Endpoints.TodoItemEndpoints;

namespace CSharp.WebApi.Mapping;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<TodoItem, TodoItemModel>().ReverseMap();

        CreateMap<CreateTodoItemRequest, TodoItemModel>().ReverseMap();
        CreateMap<CreateTodoItemRequest, TodoItem>().ReverseMap();
        CreateMap<UpdateTodoItemRequest, TodoItemModel>().ReverseMap();
        CreateMap<UpdateTodoItemRequest, TodoItem>().ReverseMap();
    }
}