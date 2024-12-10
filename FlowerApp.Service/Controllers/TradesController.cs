using AutoMapper;
using FlowerApp.Domain.Common;
using FlowerApp.DTOs.Response.Trades;
using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;
using DTOTrade = FlowerApp.DTOs.Common.Trades.Trade;
using ApplicationTrade = FlowerApp.Domain.ApplicationModels.TradeModels.Trade;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/trades")]
public class TradesController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ITradeService tradeService;

    public TradesController(ITradeService tradeService, IMapper mapper)
    {
        this.tradeService = tradeService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получение трейда по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор трейда</param>
    /// <returns>Информация о трейде</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DTOTrade>> GetTrade(Guid id)
    {
        var trade = await tradeService.Get(id);
        if (trade == null)
        {
            return NotFound("Trade not found");
        }

        return Ok(mapper.Map<DTOTrade>(trade));
    }

    /// <summary>
    ///     Получение списка трейдов с фильтрацией, сортировкой и пагинацией
    /// </summary>
    /// <remarks>
    ///     Параметры фильтрации:
    ///     - location: фильтр по локации трейда
    ///     - userId: фильтр по идентификатору пользователя (если не указан, возвращаются все трейды)
    ///     - includeUserTrades: если true, возвращает только трейды пользователя, если false — все трейды за исключением
    ///     трейдов пользователя
    ///     Параметры пагинации:
    ///     - Skip: количество пропускаемых элементов
    ///     - Take: количество возвращаемых элементов (максимум 50)
    /// </remarks>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="location">Фильтр по локации</param>
    /// <param name="userId">Фильтр по идентификатору пользователя (если не указан, возвращаются все трейды)</param>
    /// <param name="includeUserTrades">
    ///     Если `true`, возвращает только трейды пользователя, иначе — все трейды за исключением
    ///     трейдов пользователя
    /// </param>
    /// <returns>Список трейдов с основной информацией</returns>
    [HttpGet]
    public async Task<ActionResult<GetTradeResponse>> GetTrades(
        [FromQuery] Pagination pagination,
        [FromQuery] string? location,
        [FromQuery] string? userId,
        [FromQuery] bool includeUserTrades = false)
    {
        var trades = await tradeService.GetAll(pagination, location, userId, includeUserTrades);
        return Ok(trades);
    }

    /// <summary>
    ///     Создание нового трейда
    /// </summary>
    /// <param name="trade">Детали трейда для создания</param>
    /// <returns>Созданный трейд</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DTOTrade trade)
    {
        var result = await tradeService.Create(mapper.Map<ApplicationTrade>(trade));

        if (!result)
        {
            return BadRequest("Failed to create trade");
        }

        return Ok();
    }

    /// <summary>
    ///     Деактивация трейда
    /// </summary>
    /// <param name="id">Идентификатор трейда</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateTrade(Guid id)
    {
        var result = await tradeService.DeactivateTrade(id);
        if (!result)
        {
            return NotFound("Trade not found or already deactivated");
        }

        return Ok("Trade deactivated successfully");
    }

    /// <summary>
    ///     Обновление существующего трейда
    /// </summary>
    /// <param name="id">Идентификатор трейда для обновления</param>
    /// <param name="trade">Детали трейда для обновления</param>
    /// <returns>Обновленный трейд</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DTOTrade trade)
    {
        var result = await tradeService.Update(id, mapper.Map<ApplicationTrade>(trade));
        if (!result)
        {
            return NotFound("Trade not found or update failed");
        }

        return Ok(result);
    }
}