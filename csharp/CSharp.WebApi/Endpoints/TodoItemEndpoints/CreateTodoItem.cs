namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public partial class CreateTodoItem : Endpoint<CreateTodoItemRequest, TodoItemModel>
{
    [Inject] private readonly ILogger<CreateTodoItem> _logger;
    [Inject] private readonly ITodoListService _listService;
    [Inject] private readonly ITodoItemService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpPut("todoitem/create")]
    public override async Task<ActionResult<TodoItemModel>> HandleAsync(CreateTodoItemRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);
        _logger.LogInformation($"User created todoitem for list {request.ListId}");

        var userId = User.GetId();

        var list = await _listService.FindByIdAsync(request.ListId);
        if (list == null || list.OwnerId != userId)
            return BadRequest(ErrorCodes.INVALID_ID);

        var entity = _mapper.Map<TodoItem>(request);
        await _service.CreateAsync(entity, cancellationToken);
        var dto = _mapper.Map<TodoItemModel>(entity);

        return dto;
    }
}