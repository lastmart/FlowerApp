using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlowerApp.Service.Handlers;

public class GoogleAuthorizationHandler: AuthorizationHandler<GoogleAuthorizationRequirement>
{
    private readonly IGoogleAuthService googleAuthService;

    public GoogleAuthorizationHandler(IGoogleAuthService googleAuthService)
    {
        this.googleAuthService = googleAuthService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        GoogleAuthorizationRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
        {
            context.Fail();
            return;
        }

        if (!httpContext.Request.Headers.ContainsKey("Authorization"))
        {
            context.Fail();
            return;
        }

        var token = httpContext.Request.Headers["Authorization"].ToString();
        if ((await googleAuthService.GetUserByAccessToken(token)).Failure)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}

public record GoogleAuthorizationRequirement : IAuthorizationRequirement;