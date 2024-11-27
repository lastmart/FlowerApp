using FlowerApp.Domain.ApplicationModels.TradeModels;
using FlowerApp.Domain.Common;
using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/trades")]
public class TradesController : ControllerBase
{
    private readonly ITradeService tradeService;

    public TradesController(ITradeService tradeService)
    {
        this.tradeService = tradeService;
    }
    
    /// <summary>
    /// Получение трейда по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор трейда</param>
    /// <returns>Информация о трейде</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Trade>> GetTrade(Guid id)
    {
        var trade = await tradeService.Get(id);
        if (trade == null)
        {
            return NotFound("Trade not found");
        }

        return Ok(trade);
    }

    /// <summary>
    /// Получение списка трейдов с фильтрацией, сортировкой и пагинацией
    /// </summary>
    /// <remarks>
    /// Параметры фильтрации:
    /// - location: фильтр по локации трейда
    /// - userId: фильтр по идентификатору пользователя (если не указан, возвращаются все трейды)
    /// - includeUserTrades: если true, возвращает только трейды пользователя, если false — все трейды за исключением трейдов пользователя
    ///
    /// Параметры пагинации:
    /// - Skip: количество пропускаемых элементов
    /// - Take: количество возвращаемых элементов (максимум 50)
    /// </remarks>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="location">Фильтр по локации</param>
    /// <param name="userId">Фильтр по идентификатору пользователя (если не указан, возвращаются все трейды)</param>
    /// <param name="includeUserTrades">Если `true`, возвращает только трейды пользователя, иначе — все трейды за исключением трейдов пользователя</param>
    /// <returns>Список трейдов с основной информацией</returns>
    [HttpGet]
    public async Task<ActionResult<IList<Trade>>> GetTrades(
        [FromQuery] Pagination pagination,
        [FromQuery] string? location,
        [FromQuery] string? userId,
        [FromQuery] bool includeUserTrades = false)
    {
        var trades = await tradeService.GetAll(pagination, location, userId, includeUserTrades);
        return Ok(trades);
    }

    /// <summary>
    /// Создание нового трейда
    /// </summary>
    /// <param name="trade">Детали трейда для создания</param>
    /// <returns>Созданный трейд</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Trade trade)
    {
        try
        {
            var createdTrade = await tradeService.Create(trade); 

            if (createdTrade != null)
            {
                return Ok(createdTrade);
            }

            return BadRequest("Failed to create trade");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }


    /// <summary>
    /// Обновление существующего трейда
    /// </summary>
    /// <param name="id">Идентификатор трейда для обновления</param>
    /// <param name="trade">Детали трейда для обновления</param>
    /// <returns>Обновленный трейд</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Trade trade)
    {
        var updatedTrade = await tradeService.Update(id, trade);
        if (updatedTrade == null)
        {
            return NotFound("Trade not found or update failed");
        }

        return Ok(updatedTrade);
    }
}