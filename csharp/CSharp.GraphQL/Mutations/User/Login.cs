namespace CSharp.GraphQL;

public record LoginRequest
(
    string Username,
    string Password
);
public record LoginResponse
(
    bool Success,
    string? JwtToken
);
public partial class Mutation
{
    public async Task<LoginResponse> Login(
        [Service] IUserService userService,
        [Service] IPasswordVerifier passwordHashVerifier,
        [Service] IJwtTokenGenerator jwtTokenGenerator,
        LoginRequest request,
        CancellationToken token = default
    )
    {
        var SignInError = new LoginResponse(Success: false, JwtToken: null);

        var user = await userService.FindByUsernameAsync(request.Username);
        if (user == null)
            return SignInError;

        if (passwordHashVerifier.VerifyHash(request.Password, user.PasswordHash) == false)
            return SignInError;

        string jwtToken = jwtTokenGenerator.GenerateJwtToken(user);
        return new LoginResponse(Success: true, jwtToken);
    }
}