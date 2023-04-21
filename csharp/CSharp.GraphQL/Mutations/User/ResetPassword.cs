namespace CSharp.GraphQL;

public record ResetPasswordRequest
(
    string Email,
    string Code,
    string NewPassword
);
public record ResetPasswordResponse
(
    bool Success,
    string? JwtToken
);
public partial class Mutation
{
    public async Task<ResetPasswordResponse> ResetPassword(
        [Service] IUserService userService,
        [Service] IPasswordHasher passwordHasher,
        [Service] IJwtTokenGenerator jwtTokenGenerator,
        ResetPasswordRequest request,
        CancellationToken token = default
    )
    {
        var ResetPasswordError = new ResetPasswordResponse(Success: false, JwtToken: null);

        var user = await userService.FindByEmailAsync(request.Email);
        if (user == null || user.ResetPasswordCode != request.Code)
            return ResetPasswordError;

        user.ResetPasswordCode = String.Empty;
        user.ResetPasswordRequestAt = null;
        user.PasswordHash = passwordHasher.Hash(request.NewPassword);
        await userService.UpdateAsync(user, token);

        var jwtToken = jwtTokenGenerator.GenerateJwtToken(user);
        return new ResetPasswordResponse(Success: true, jwtToken);
    }
}