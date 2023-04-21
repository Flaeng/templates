namespace CSharp.WebApi.Endpoints.TodoItemEndpoints;

public partial class DeleteTodoItem : EndpointWithRequest<IdRequest>
{
    [Inject] private readonly ILogger<DeleteTodoItem> _logger;
    [Inject] private readonly ITodoItemService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpDelete("todoitem/delete/{id}")]
    public override async Task<ActionResult> HandleAsync([FromRoute] IdRequest request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetId();

        var entity = await _service.FindByIdAsync(request.Id);
        if (entity == null || entity.List?.OwnerId != userId)
            return BadRequest(ErrorCodes.INVALID_ID);

        await _service.DeleteAsync(entity.Id, cancellationToken);
        return Ok();
    }
}