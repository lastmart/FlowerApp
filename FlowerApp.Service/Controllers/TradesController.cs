using AutoMapper;
using FlowerApp.Domain.Models.Operation;
using FlowerApp.DTOs.Common;
using FlowerApp.DTOs.Response.Trades;
using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;
using DTOTrade = FlowerApp.DTOs.Common.Trades.Trade;
using ApplicationTrade = FlowerApp.Domain.Models.TradeModels.Trade;

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
    public async Task<ActionResult<DTOTrade>> GetTrade(int id)
    {
        var trade = await tradeService.Get(id);
        if (trade == null)
        {
            return NotFound("Trade not found");
        }

        return Ok(mapper.Map<DTOTrade>(trade));
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
        return result switch
        {
            OperationResult.Success => Ok(),
            OperationResult.NotFound => NotFound("User not found"),
            OperationResult.InvalidData => BadRequest("User must have either email or telegram specified"),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }

    /// <summary>
    ///     Деактивация трейда
    /// </summary>
    /// <param name="id">Идентификатор трейда</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateTrade(int id)
    {
        var result = await tradeService.DeactivateTrade(id);
        return result switch
        {
            OperationResult.Success => Ok(),
            OperationResult.NotFound => NotFound("Trade not found"),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }

    /// <summary>
    ///     Обновление существующего трейда
    /// </summary>
    /// <param name="id">Идентификатор трейда для обновления</param>
    /// <param name="trade">Детали трейда для обновления</param>
    /// <returns>Обновленный трейд</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DTOTrade trade)
    {
        var result = await tradeService.Update(mapper.Map<ApplicationTrade>(trade));
        
        return result switch
        {
            OperationResult.Success => Ok("Trade deactivated successfully"),
            OperationResult.NotFound => NotFound("Trade not found"),
            _ => StatusCode(500, "An unexpected error occurred")
        };
    }
    
    /// <summary>
    ///     Получение всех трейдов конкретного пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="pagination">Параметры пагинации</param>
    /// <param name="location">Локация для фильтрации (опционально)</param>
    /// <returns>Список трейдов пользователя с общим количеством</returns>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<GetTradeResponse>> GetUserTrades(
        int userId,
        [FromQuery] Pagination pagination,
        [FromQuery] string? location = null)
    {
        var response = await tradeService.GetUserTrades(pagination, location, userId);
        var trades = response.Trades.Select(t => mapper.Map<DTOTrade>(t));
        return Ok(new GetTradeResponse(response.Count, trades));
    }
    
    /// <summary>
    ///     Получение всех трейдов, кроме трейдов указанного пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя, чьи трейды нужно исключить</param>
    /// <param name="pagination">Параметры пагинации</param>
    /// <param name="location">Локация для фильтрации (опционально)</param>
    /// <returns>Список трейдов других пользователей с общим количеством</returns>
    [HttpGet("others")]
    public async Task<ActionResult<GetTradeResponse>> GetOtherUsersTrades(
        [FromQuery] Pagination pagination,
        [FromQuery] int? userId = null,
        [FromQuery] string? location = null)
    {
        var response = await tradeService.GetOtherUsersTrades(pagination, location, userId);
        var trades = response.Trades.Select(t => mapper.Map<DTOTrade>(t));
        return Ok(new GetTradeResponse(response.Count, trades));
    }
}