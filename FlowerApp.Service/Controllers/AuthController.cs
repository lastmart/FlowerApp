using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("auth")]
public class AuthController
{
    private readonly IGoogleAuthService googleAuthService;

    public AuthController(IGoogleAuthService googleAuthService)
    {
        this.googleAuthService = googleAuthService;
    }

    [HttpPost("google/{authCode}")]
    public async Task<IActionResult> GoogleLogin([FromRoute] string authCode)
    {
        var response = await googleAuthService.TryAuthenticateUserAsync(authCode);
        if (response.Failure) return new Unauthorized();

        return Ok(response);
    }
}