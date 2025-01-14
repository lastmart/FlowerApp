using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/me")]
public class MeController : ControllerBase
{
    private readonly IAuthorizationContext authorizationContext;
    private readonly IUserService userService;

    public MeController(IAuthorizationContext authorizationContext, IUserService userService)
    {
        this.authorizationContext = authorizationContext;
        this.userService = userService;
    }

    [HttpGet("info")]
    public async Task<ActionResult> GetUserInfo()
    {
        var userId = await authorizationContext.GetGoogleIdFromAccessToken();
        var user = await userService.Get(userId);
        return Ok(user);
    }
}