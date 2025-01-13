using FlowerApp.Service.Services;
using Kontur.Results;
using Microsoft.AspNetCore.Mvc;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IGoogleAuthService googleAuthService;

    public AuthController(IGoogleAuthService googleAuthService)
    {
        this.googleAuthService = googleAuthService;
    }

    /// <summary>
    ///     Получить google access token по auth code
    /// </summary>
    /// <param name="authCode">Известный google auth code</param>
    /// <returns>Google access token</returns>
    [HttpGet("google/{authCode}")]
    public async Task<IActionResult> GoogleLogin([FromRoute] string authCode)
    {
        
        var decodedAuthCode = Uri.UnescapeDataString(authCode);
        var response = await googleAuthService.TryAuthenticateUserAsync(decodedAuthCode);
        if (response.Failure) return Unauthorized();

        return Ok(response.GetValueOrThrow());
    }
}