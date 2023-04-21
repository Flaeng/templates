namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public partial class ByIdTodoList : Endpoint<IdRequest, TodoListModel>
{
    [Inject] private readonly ILogger<ByIdTodoList> _logger;
    [Inject] private readonly ITodoListService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpGet("todolist/by-id")]
    public override async Task<ActionResult<TodoListModel>> HandleAsync(IdRequest request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetId();
        var result = await _service
            .Query()
            .WhereOwnerIs(userId)
            .WhereIdIs(request.Id)
            .ProjectTo<TodoListModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return result is null ? BadRequest(ErrorCodes.INVALID_ID) : result;
    }
}
