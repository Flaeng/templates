namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public partial class ListTodoItem : Endpoint<IdRequest, List<TodoItemModel>>
{
    [Inject] private readonly ILogger<ListTodoItem> _logger;
    [Inject] private readonly ITodoItemService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpGet("todoitem/list")]
    public override async Task<ActionResult<List<TodoItemModel>>> HandleAsync(IdRequest request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetId();
        var result = await _service
            .Query()
            .InList(request.Id)
            .WhereOwnerIs(userId)
            .AsAsyncEnumerable()
            .Select(_mapper.Map<TodoItemModel>)
            .ToListAsync(cancellationToken);
        return result;
    }
}
