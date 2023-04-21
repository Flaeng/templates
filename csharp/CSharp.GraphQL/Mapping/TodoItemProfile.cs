using AutoMapper;

using CSharp.GraphQL.Models;

namespace CSharp.GraphQL.Mapping;

public class TodoItemProfile : Profile
{
    public TodoItemProfile()
    {
        CreateMap<TodoItem, TodoItemModel>()
            .ReverseMap();

        CreateMap<EditTodoItemModel, TodoItemModel>();
    }
}