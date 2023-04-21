namespace CSharp.WebApi.Endpoints.Authorization;

public partial class VerifyEmail : EndpointBaseAsync
    .WithRequest<VerifyEmailRequest>.WithActionResult<VerifyEmailResponse>
{
    [Inject] private readonly ILogger<VerifyEmail> _logger;
    [Inject] private readonly IUserService _service;
    [Inject] private readonly IJwtTokenGenerator _jwtTokenGenerator;

    [HttpPost("auth/verify-email")]
    public override async Task<ActionResult<VerifyEmailResponse>> HandleAsync(VerifyEmailRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);
        _logger.LogInformation($"User creation attempt registered");

        var user = await _service.FindByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return VerifyEmailResponse.UnsuccessResponse;

        user.VerifiedEmailAt = DateTimeOffset.UtcNow;
        user.VerificationCode = null;
        await _service.UpdateAsync(user, cancellationToken);

        var token = _jwtTokenGenerator.GenerateJwtToken(user);
        return VerifyEmailResponse.SuccessResponse(token);
    }
}