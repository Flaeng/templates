namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public partial class UpdateTodoList : Endpoint<UpdateTodoListRequest, TodoListModel>
{
    [Inject] private readonly ILogger<UpdateTodoList> _logger;
    [Inject] private readonly ITodoItemService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpPatch("todolist/update")]
    public override async Task<ActionResult<TodoListModel>> HandleAsync(UpdateTodoListRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        var entity = await _service.FindByIdAsync(request.Id);
        if (entity == null)
            return BadRequest(ErrorCodes.INVALID_ID);

        _mapper.Map(request, entity);
        await _service.Context.SaveChangesAsync(cancellationToken);
        var dto = _mapper.Map<TodoListModel>(entity);

        return dto;
    }
}