using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetId(this ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            throw new Exception("NameIdentifier claim not found");

        return Guid.Parse(userIdClaim.Value);
    }
    public static bool TryGetId(this ClaimsPrincipal principal, out Guid userId)
    {
        userId = Guid.Empty;
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return false;

        return Guid.TryParse(userIdClaim.Value, out userId);
    }
}