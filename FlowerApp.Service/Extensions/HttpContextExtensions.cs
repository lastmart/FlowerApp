namespace FlowerApp.Service.Extensions;

public static class HttpContextExtensions
{
    public static string GetAccessTokenFromHeaderAuthorization(this HttpContext httpContext)
    {
        const string schemeBearerName = "Bearer ";

        var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authorizationHeader) ||
            !authorizationHeader.StartsWith(schemeBearerName, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentNullException("Can't get accessToken from Authorization header");
        }

        return authorizationHeader[schemeBearerName.Length..].Trim();
    }
}