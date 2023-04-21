namespace CSharp.WebApi.Endpoints.Authorization;

public class VerifyEmailRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Code { get; set; } = "";
}