namespace CSharp.WebApi.Endpoints.Authorization;

public partial class Login : EndpointBaseAsync
    .WithRequest<LoginRequest>.WithActionResult<LoginResponse>
{
    [Inject] private readonly ILogger<Login> _logger;
    [Inject] private readonly IUserService _service;
    [Inject] private readonly IPasswordVerifier _passwordVerifier;
    [Inject] private readonly IJwtTokenGenerator _jwtTokenGenerator;

    [HttpPost("auth/login")]
    public override async Task<ActionResult<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);
        _logger.LogInformation($"Login attempt registered");

        var user = await _service.FindByEmailAsync(request.Email, cancellationToken);
        if (user is null || user.VerifiedEmailAt.HasValue == false)
            return LoginResponse.UnsuccessResponse;
        _logger.LogInformation($"Login attempt registered for user with id {user.Id}");

        if (_passwordVerifier.VerifyHash(request.Password, user.PasswordHash) == false)
            return LoginResponse.UnsuccessResponse;
        _logger.LogInformation($"Password validation succeeded");

        var token = _jwtTokenGenerator.GenerateJwtToken(user);
        return LoginResponse.SuccessResponse(token);
    }
}