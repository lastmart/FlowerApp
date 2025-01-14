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
            return NotFound(new FailureOperationStatus
            {
                Code = "TradeNotFound",
                Message = "No trade found with the given ID"
            });
        }

        return Ok(new SuccessOperationStatus<DTOTrade>
        {
            Code = "Ok",
            Message = "",
            Data = mapper.Map<DTOTrade>(trade)
        });
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
            OperationResult.Success => Ok(new SuccessOperationStatus<object> { Code = "Ok", Message = "Trade created successfully"}),
            OperationResult.NotFound => NotFound(new FailureOperationStatus { Code = "UserNotFound", Message = "User not found" }),
            OperationResult.InvalidData => BadRequest(new FailureOperationStatus { Code = "InvalidData", Message = "User must have either email or telegram specified" }),
            _ => StatusCode(500, new FailureOperationStatus { Code = "InternalServerError", Message = "An unexpected error occurred" })
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
            OperationResult.Success => Ok(new SuccessOperationStatus<object> { Code = "Ok", Message = "Trade deactivated successfully" }),
            OperationResult.NotFound => NotFound(new FailureOperationStatus { Code = "TradeNotFound", Message = "No exchange was found for this id" }),
            _ => StatusCode(500, new FailureOperationStatus { Code = "InternalServerError", Message = "An unexpected error occurred" })
        };
    }

    /// <summary>
    ///     Обновление существующего трейда
    /// </summary>
    /// <param name="trade">Детали трейда для обновления</param>
    /// <returns>Обновленный трейд</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DTOTrade trade)
    {
        var result = await tradeService.Update(mapper.Map<ApplicationTrade>(trade));
        
        return result switch
        {
            OperationResult.Success => Ok(new SuccessOperationStatus<object> { Code = "Ok", Message = "Trade updated successfully" }),
            OperationResult.NotFound => NotFound(new FailureOperationStatus { Code = "TradeNotFound", Message = "Trade or associated user not found" }),
            _ => StatusCode(500, new FailureOperationStatus { Code = "InternalServerError", Message = "An unexpected error occurred" })
        };
    }
    
    /// <summary>
    ///     Получение всех трейдов конкретного пользователя
    /// </summary>
    /// <param name="googleId">Идентификатор пользователя</param>
    /// <param name="pagination">Параметры пагинации</param>
    /// <param name="location">Локация для фильтрации (опционально)</param>
    /// <returns>Список трейдов пользователя с общим количеством</returns>
    [HttpGet("user/{googleId}")]
    public async Task<ActionResult<IRepositoryOperationStatus>> GetUserTrades(
        string googleId,
        [FromQuery] Pagination pagination,
        [FromQuery] string? location = null
    )
    {
        var response = await tradeService.GetUserTrades(pagination, location, googleId);
        if (response.Result == OperationResult.NotFound )
        {
            return NotFound(new FailureOperationStatus
            {
                Code = "UserNotFound",
                Message = $"User with ID {googleId} not found."
            });
        }
        var trades = response.Data?.Trades.Select(t => mapper.Map<DTOTrade>(t));
        return Ok(new SuccessOperationStatus<GetTradeResponse>
        {
            Code = "Ok",
            Message = "",
            Data = new GetTradeResponse(response.Data?.Count ?? 0, trades)
        });
    }
    
    /// <summary>
    ///     Получение всех трейдов, кроме трейдов указанного пользователя
    /// </summary>
    /// <param name="googleId">Идентификатор пользователя, чьи трейды нужно исключить</param>
    /// <param name="pagination">Параметры пагинации</param>
    /// <param name="location">Локация для фильтрации (опционально)</param>
    /// <returns>Список трейдов других пользователей с общим количеством</returns>
    [HttpGet("others")]
    public async Task<ActionResult<IRepositoryOperationStatus>> GetOtherUsersTrades(
        [FromQuery] Pagination pagination,
        [FromQuery] string? googleId = null,
        [FromQuery] string? location = null)
    {
        var response = await tradeService.GetOtherUsersTrades(pagination, location, googleId);
        if (response.Result == OperationResult.NotFound)
        {
            return NotFound(new FailureOperationStatus
            {
                Code = "UserNotFound",
                Message = $"User with ID {googleId} not found."
            });
        }
        
        var trades = response.Data?.Trades.Select(t => mapper.Map<DTOTrade>(t));
        return new SuccessOperationStatus<GetTradeResponse>
        {
            Code = "Ok",
            Message = "",
            Data = new GetTradeResponse(response.Data?.Count ?? 0, trades)
        };
    }
}