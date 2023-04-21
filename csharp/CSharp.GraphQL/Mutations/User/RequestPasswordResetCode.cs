namespace CSharp.GraphQL;

public record RequestPasswordResetCodeRequest
(
    string Email
);
public record RequestPasswordResetCodeResponse(bool Success);
public partial class Mutation
{
    public async Task<RequestPasswordResetCodeResponse> RequestPasswordResetCode(
        [Service] IUserService userService,
        [Service] IStringGenerator stringGenerator,
        RequestPasswordResetCodeRequest request,
        CancellationToken token = default
    )
    {
        var user = await userService.FindByEmailAsync(request.Email);
        if (user == null)
            return new RequestPasswordResetCodeResponse(true);

        string resetCode = stringGenerator.GenerateString(
            minLength: 8,
            maxLength: 16,
            characters: Characters.Letters
                        | Characters.Numbers
                        | Characters.SpecialCharacters);

        user.ResetPasswordCode = resetCode;
        user.ResetPasswordRequestAt = DateTimeOffset.Now;
        await userService.UpdateAsync(user, token);

        return new RequestPasswordResetCodeResponse(true);
    }
}