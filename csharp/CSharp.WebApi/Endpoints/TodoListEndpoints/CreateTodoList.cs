namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public partial class CreateTodoList : Endpoint<CreateTodoListRequest, TodoListModel>
{
    [Inject] private readonly ILogger<CreateTodoList> _logger;
    [Inject] private readonly IUserService _userService;
    [Inject] private readonly ITodoListService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpPut("todolist/create")]
    public override async Task<ActionResult<TodoListModel>> HandleAsync(CreateTodoListRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        var entity = _mapper.Map<TodoList>(request);
        entity.OwnerId = User.GetId();
        await _service.CreateAsync(entity, cancellationToken);
        var dto = _mapper.Map<TodoListModel>(entity);

        return dto;
    }
}