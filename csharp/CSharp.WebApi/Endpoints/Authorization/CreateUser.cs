namespace CSharp.WebApi.Endpoints.Authorization;

public partial class CreateUser : EndpointBaseAsync
    .WithRequest<CreateUserRequest>.WithActionResult
{
    [Inject] private readonly ILogger<CreateUser> _logger;
    [Inject] private readonly IUserService _service;
    [Inject] private readonly IPasswordHasher _passwordHasher;
    [Inject] private readonly IMailProvider _mailProvider;
    [Inject] private readonly IStringGenerator _stringGenerator;

    [HttpPost("auth/create-user")]
    public override async Task<ActionResult> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        if (ModelState.IsValid == false)
            return BadRequest(ModelState);
        _logger.LogInformation($"User creation attempt registered");

        var user = await _service.FindByEmailAsync(request.Email, cancellationToken);
        if (user is not null)
            return Ok();

        string verficationCode = _stringGenerator.GenerateString(
            length: 10,
            characters: Characters.Letters | Characters.Numbers
        );

        user = new();
        user.Email = request.Email;
        user.Username = request.Name;
        user.PasswordHash = _passwordHasher.Hash(request.Password);
        user.VerificationCode = verficationCode;
        await _service.CreateAsync(user, cancellationToken);

        var mailParameters = new Dictionary<string, string>()
        {
            { MailTemplate.UserName, request.Name },
            { MailTemplate.WelcomeMail_VerificationCode, verficationCode }
        };
        await _mailProvider.SendWelcomeMailAsync(user.Email, mailParameters);

        return Ok();
    }
}