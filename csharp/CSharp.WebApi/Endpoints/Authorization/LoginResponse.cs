namespace CSharp.WebApi.Endpoints.Authorization;

public class LoginResponse
{
    public static LoginResponse UnsuccessResponse = new LoginResponse(false, null);
    
    public bool Success { get; set; }
    public string? Token { get; set; }

    protected LoginResponse(bool Success, string? Token)
    {
        this.Success = Success;
        this.Token = Token;
    }

    public static LoginResponse SuccessResponse(string token)
        => new LoginResponse(true, token);
}