using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetId(this ClaimsPrincipal principal, out Guid userId)
    {
        userId = Guid.Empty;
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return false;

        return Guid.TryParse(userIdClaim.Value, out userId);
    }
}