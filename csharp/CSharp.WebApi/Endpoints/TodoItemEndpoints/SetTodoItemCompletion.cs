namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public partial class SetTodoItemCompletion : Endpoint<SetTodoItemCompletionRequest, TodoItemModel>
{
    [Inject] private readonly ILogger<SetTodoItemCompletion> _logger;
    [Inject] private readonly ITodoItemService _service;
    [Inject] private readonly IMapper _mapper;


    [Authorize, HttpPost("todoitem/set-completion")]
    public override async Task<ActionResult<TodoItemModel>> HandleAsync(SetTodoItemCompletionRequest request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetId();

        var entity = await _service.FindByIdAsync(request.Id);
        if (entity == null || entity.List?.OwnerId != userId)
            return BadRequest(ErrorCodes.INVALID_ID);

        _service.SetCompletionAsync(request.Id, request.Completed, cancellationToken);
        await _service.Context.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<TodoItemModel>(entity);
        return dto;
    }
}
