namespace CSharp.Infrastructure.Providers;

public interface IPasswordHasher
{
    string Hash(string raw);
}
public interface IPasswordVerifier
{
    bool VerifyHash(string raw, string hash);
}
public class PasswordProvider : IPasswordHasher, IPasswordVerifier
{
    public string Hash(string raw)
    {
        return BCrypt.Net.BCrypt.HashPassword(raw);
    }

    public bool VerifyHash(string raw, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(raw, hash);
    }
}