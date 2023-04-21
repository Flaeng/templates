namespace CSharp.GraphQL;

public record DeleteListModel
(
    Guid Id
);
public partial class Mutation
{
    public async Task DeleteList(
        [Service] ITodoListService service,
        DeleteListModel model,
        CancellationToken token = default
    )
    {
        await service.DeleteAsync(model.Id, token);
    }
}