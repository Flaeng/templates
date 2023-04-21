using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CSharp.Domain.Entities;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CSharp.Infrastructure.Providers;

public class JwtOptions
{
    public string Audience { get; set; } = "";
    public string Issuer { get; set; } = "";
    public string Key { get; set; } = "";
}
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user, string[] roles);
}
public static class IJwtTokenGeneratorExtensions
{
    public static string GenerateJwtToken(this IJwtTokenGenerator generator, User user)
        => generator.GenerateJwtToken(user, new string[0]);
}
public interface IJwtTokenValidator
{
    bool TryReadJwtToken(string token, out ClaimsIdentity identity);
}
public class JwtTokenProvider : IJwtTokenGenerator, IJwtTokenValidator
{
    public const char ROLES_SEPARATOR = ',';
    private readonly JwtOptions options;

    public JwtTokenProvider(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
    }

    public string GenerateJwtToken(User user, string[] roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.Username));
        claims.Add(new Claim(ClaimTypes.Role, String.Join(ROLES_SEPARATOR, roles)));

        var token = new JwtSecurityToken(
            options.Issuer,
            options.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool TryReadJwtToken(string token, out ClaimsIdentity identity)
    {
        identity = new ClaimsIdentity();

        var handler = new JwtSecurityTokenHandler();
        if (handler.CanReadToken(token) == false)
            return false;

        var jwtToken = handler.ReadJwtToken(token);
        identity = new ClaimsIdentity(
            jwtToken.Claims,
            "Jwt",
            ClaimTypes.Name,
            ClaimTypes.Role
        );
        return true;
    }
}