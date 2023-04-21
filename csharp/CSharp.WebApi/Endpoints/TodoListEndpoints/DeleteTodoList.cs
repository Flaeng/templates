namespace CSharp.WebApi.Endpoints.TodoListEndpoints;

public partial class DeleteTodoList : EndpointWithRequest<IdRequest>
{
    [Inject] private readonly ILogger<DeleteTodoList> _logger;
    [Inject] private readonly ITodoListService _service;
    [Inject] private readonly IMapper _mapper;

    [Authorize, HttpDelete("todolist/delete")]
    public override async Task<ActionResult> HandleAsync([FromRoute] IdRequest request, CancellationToken cancellationToken = default)
    {
        if (User.TryGetId(out var userId) == false)
            return Unauthorized();

        var entity = await _service.FindByIdAsync(request.Id);
        if (entity == null || entity.OwnerId != userId)
            return BadRequest(ErrorCodes.INVALID_ID);

        await _service.DeleteAsync(entity.Id, cancellationToken);
        return Ok();
    }
}