namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public partial class ListTodoList : EndpointWithResponse<List<TodoListModel>>
{
    [Inject] private readonly ILogger<ListTodoList> _logger;
    [Inject] private readonly ITodoListService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpGet("todolist/list")]
    public override async Task<ActionResult<List<TodoListModel>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var userId = User.GetId();
        return await _service
            .Query()
            .WhereOwnerIs(userId)
            .ProjectTo<TodoListModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
