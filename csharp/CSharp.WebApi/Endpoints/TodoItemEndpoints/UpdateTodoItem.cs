namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public partial class UpdateTodoItem : Endpoint<UpdateTodoItemRequest, TodoItemModel>
{
    [Inject] private readonly ILogger<UpdateTodoItem> _logger;
    [Inject] private readonly ITodoItemService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpPatch("todoitem/update")]
    public override async Task<ActionResult<TodoItemModel>> HandleAsync(UpdateTodoItemRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);

        var userId = User.GetId();

        var entity = await _service.FindByIdAsync(request.Id);
        if (entity == null || entity.List?.OwnerId != userId)
            return NotFound();

        _mapper.Map(request, entity);
        await _service.Context.SaveChangesAsync(cancellationToken);
        var dto = _mapper.Map<TodoItemModel>(entity);

        return dto;
    }
}