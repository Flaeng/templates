namespace CSharp.WebApi.Endpoints.Authorization;

public class VerifyEmailResponse : LoginResponse
{
    public static new VerifyEmailResponse UnsuccessResponse = new VerifyEmailResponse(false, null);

    protected VerifyEmailResponse(bool Success, string? Token) : base(Success, Token)
    { }

    public static new VerifyEmailResponse SuccessResponse(string token)
        => new VerifyEmailResponse(true, token);

}