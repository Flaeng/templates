namespace CSharp.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Email { get; set; } = "";
    public string? VerificationCode { get; set; }
    public DateTimeOffset? VerifiedEmailAt { get; set; }
    public string? ResetPasswordCode { get; set; } = "";
    public DateTimeOffset? ResetPasswordRequestAt { get; set; }

    public List<TodoList> Lists { get; set; } = new();
}