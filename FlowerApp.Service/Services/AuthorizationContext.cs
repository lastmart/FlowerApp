using FlowerApp.Service.Extensions;
using Kontur.Results;

namespace FlowerApp.Service.Services;

public class AuthorizationContext : IAuthorizationContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IGoogleAuthService googleAuthService;

    public AuthorizationContext(IHttpContextAccessor httpContextAccessor, IGoogleAuthService googleAuthService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.googleAuthService = googleAuthService;
    }

    public async Task<string> GetGoogleIdFromAccessToken()
    {
        var accessToken = httpContextAccessor.HttpContext?.GetAccessTokenFromHeaderAuthorization();
        if (accessToken is null)
        {
            throw new ArgumentNullException("HttpContext is null");
        }

        return (await googleAuthService.GetUserGoogleIdByAccessToken(accessToken)).GetValueOrThrow(
            new ArgumentNullException("Can't get user googleId by accessToken"));
    }
}