namespace CSharp.Infrastructure.Tests.Providers;

public class JwtTokenProviderTests
{
    JwtTokenProvider? provider;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = new Mock<IOptions<JwtOptions>>();
        string JWT_KEY = Guid.NewGuid().ToString("N")
            + Guid.NewGuid().ToString("N")
            + Guid.NewGuid().ToString("N")
            + Guid.NewGuid().ToString("N");
        options
            .Setup(x => x.Value)
            .Returns(new JwtOptions
            {
                Audience = "AUDIENCE", 
                Issuer = "ISSUER", 
                Key = JWT_KEY
            });
        provider = new JwtTokenProvider(options.Object);
    }

    [Test]
    public void Can_generate_jwt_token_from_user()
    {
        // Arrange
        var user = new User
        {
            Username = "JohnDoe"
        };

        // Act
        TestHelper.ThrowIfNull(provider);
        var token = provider.GenerateJwtToken(user);

        // Assert
        Assert.Less(200, token.Length);
        Assert.IsTrue(provider.TryReadJwtToken(token, out var ident));
        Assert.That(ident.Name, Is.EqualTo("JohnDoe"));
    }

    [Test]
    public void Can_read_jwt_token()
    {
        // Arrange
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJKb2huRG9lIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiIiwiZXhwIjoxNjcxNDg4MTM0LCJpc3MiOiJJU1NVRVIiLCJhdWQiOiJJU1NVRVIifQ.cobwi9fta62xX7BuCbxgH0pKtxDpCK7-td8m4AcIQ24";

        // Act
        TestHelper.ThrowIfNull(provider);
        var result = provider.TryReadJwtToken(token, out var ident);

        // Assert
        Assert.IsTrue(result);
        Assert.That(ident.Name, Is.EqualTo("JohnDoe"));
    }

}