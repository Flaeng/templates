namespace CSharp.WebApi.Endpoints;

[Authorize]
public abstract class EndpointWithRequest<TRequest> : EndpointBaseAsync
                        .WithRequest<TRequest>
                        .WithActionResult
{
}
[Authorize]
public abstract class EndpointWithResponse<TResponse> : EndpointBaseAsync
                        .WithoutRequest
                        .WithActionResult<TResponse>
{
}
[Authorize]
public abstract class Endpoint<TRequest, TResponse> : EndpointBaseAsync
                        .WithRequest<TRequest>
                        .WithActionResult<TResponse>
{
}