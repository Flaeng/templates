namespace CSharp.WebApi.Endpoints.Authorization;

public class CreateUserRequest
{
    [Required, MinLength(3), MaxLength(150)]
    public string Name { get; set; } = "";

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}