using AutoMapper;
using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;
using ApplicationUser = FlowerApp.Domain.Models.UserModels.User;
using DTOUser = FlowerApp.DTOs.Common.Users.User;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IUserService userService;

    public UsersController(IUserService userService, IMapper mapper)
    {
        this.userService = userService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Обновить информацию о пользователе по GoogleId
    /// </summary>
    /// <param name="googleId">GoogleId пользователя</param>
    /// <param name="userDto">Информация для обновления пользователя, все поля опциональные</param>
    /// <returns>Информация об обновленном пользователе</returns>
    [HttpPut("{googleId}")]
    public async Task<IActionResult> UpdateByGoogleId(string googleId, [FromBody] DTOUser userDto)
    {
        var existingUser = await userService.Get(googleId);
        if (existingUser == null)
            return NotFound("User with the specified GoogleId not found");

        var userToUpdate = mapper.Map<ApplicationUser>(userDto);
        var updatedUser = await userService.Update(googleId, userToUpdate);

        return updatedUser != null
            ? Ok("User updated")
            : StatusCode(500, "Failed to update user");
    }
    
    /// <summary>
    /// Получить пользователя по GoogleId
    /// </summary>
    /// <param name="googleId">GoogleId пользователя</param>
    /// <returns>Информация о пользователе</returns>
    [HttpGet("{googleId}")]
    public async Task<IActionResult> GetByGoogleId(string googleId)
    {
        var user = await userService.Get(googleId);
        if (user == null)
            return NotFound("User with the specified GoogleId not found");

        return Ok(user);
    }
}