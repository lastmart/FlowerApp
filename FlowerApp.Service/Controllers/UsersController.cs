// using AutoMapper;
// using FlowerApp.Service.Services;
// using Microsoft.AspNetCore.Mvc;
// using ApplicationUser = FlowerApp.Domain.Models.UserModels.User;
// using DTOUser = FlowerApp.DTOs.Common.Users.User;
//
// namespace FlowerApp.Service.Controllers;
//
// [ApiController]
// [Route("api/users")]
// public class UsersController : ControllerBase
// {
//     private readonly IMapper mapper;
//     private readonly IUserService userService;
//
//     public UsersController(IUserService userService, IMapper mapper)
//     {
//         this.userService = userService;
//         this.mapper = mapper;
//     }
//
//     /// <summary>
//     ///     Получить пользователя по идентификатору
//     /// </summary>
//     /// <param name="id">Идентификатор пользователя</param>
//     /// <returns>Детальная информация о пользователе</returns>
//     [HttpGet("{id:guid}")]
//     public async Task<IActionResult> Get(Guid id)
//     {
//         var user = await userService.Get(id);
//         return user != null ? Ok(mapper.Map<DTOUser>(user)) : NotFound();
//     }
//
//     /// <summary>
//     ///     Создать нового пользователя
//     /// </summary>
//     /// <param name="user">Информация о новом пользователе</param>
//     /// <returns>Детальная информация о созданном пользователе</returns>
//     [HttpPost]
//     public async Task<IActionResult> Create([FromBody] DTOUser user)
//     {
//         var createdUser = await userService.Create(mapper.Map<ApplicationUser>(user));
//         if (createdUser != null) return Ok(createdUser);
//
//         return BadRequest("Failed to create user");
//     }
//
//     /// <summary>
//     ///     Обновить информацию о пользователе
//     /// </summary>
//     /// <param name="id">Идентификатор пользователя</param>
//     /// <param name="user">Информация для обновления пользователя</param>
//     /// <returns>Информация о обновленном пользователе</returns>
//     [HttpPut("{id:guid}")]
//     public async Task<IActionResult> Update(Guid id, [FromBody] DTOUser user)
//     {
//         var updatedUser = await userService.Update(id, mapper.Map<ApplicationUser>(user));
//         if (updatedUser != null) return Ok(mapper.Map<DTOUser>(updatedUser));
//
//         return NotFound("User not found");
//     }
// }