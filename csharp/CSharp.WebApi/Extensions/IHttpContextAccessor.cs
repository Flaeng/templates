public static class IHttpContextAccessorExtensions
{
    public static bool TryGetRequesterUserId(
        this IHttpContextAccessor httpContextAccessor,
        out Guid userId
    )
    {
        userId = Guid.Empty;
        if (httpContextAccessor.HttpContext == null)
            return false;

        return httpContextAccessor.HttpContext.User.TryGetId(out userId);
    }
}