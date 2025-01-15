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
    private readonly IAuthorizationContext authorizationContext;

    public TradesController(ITradeService tradeService, IMapper mapper, IAuthorizationContext authorizationContext)
    {
        this.tradeService = tradeService;
        this.mapper = mapper;
        this.authorizationContext = authorizationContext;
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
        try
        {
            var googleId = await authorizationContext.GetGoogleIdFromAccessToken();
            if (string.IsNullOrEmpty(googleId))
            {
                return Unauthorized(new FailureOperationStatus
                {
                    Code = "Unauthorized",
                    Message = "Invalid or missing access token"
                });
            }
            
            var applicationTrade = mapper.Map<ApplicationTrade>(trade);
            applicationTrade.UserId = googleId;
            var result = await tradeService.Create(applicationTrade);
            return result switch
            {
                OperationResult.Success => Ok(new SuccessOperationStatus<object>
                    { Code = "Ok", Message = "Trade created successfully" }),
                OperationResult.NotFound => NotFound(new FailureOperationStatus
                    { Code = "UserNotFound", Message = "User not found" }),
                OperationResult.InvalidData => BadRequest(new FailureOperationStatus
                    { Code = "InvalidData", Message = "User must have either email or telegram specified" }),
                _ => StatusCode(500,
                    new FailureOperationStatus
                        { Code = "InternalServerError", Message = "An unexpected error occurred" })
            };
        }
        catch(ArgumentNullException ex)
        {
            return StatusCode(500, new FailureOperationStatus
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    /// <summary>
    ///     Деактивация трейда
    /// </summary>
    /// <param name="id">Идентификатор трейда</param>
    /// <returns>Результат операции</returns>
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateTrade(int id)
    {
        try
        {
            var googleId = await authorizationContext.GetGoogleIdFromAccessToken();
            if (string.IsNullOrEmpty(googleId))
            {
                return Unauthorized(new FailureOperationStatus
                {
                    Code = "Unauthorized",
                    Message = "Invalid or missing access token"
                });
            }

            var trade = await tradeService.Get(id);
            if (trade == null)
            {
                return NotFound(new FailureOperationStatus
                {
                    Code = "TradeNotFound",
                    Message = "No trade found with the given ID"
                });
            }

            if (trade.UserId != googleId)
            {
                return Unauthorized(new FailureOperationStatus
                {
                    Code = "Unauthorized",
                    Message = "You do not have permission to deactivate this trade"
                });
            }

            var result = await tradeService.DeactivateTrade(id);
            return result switch
            {
                OperationResult.Success => Ok(new SuccessOperationStatus<object>
                    { Code = "Ok", Message = "Trade deactivated successfully" }),
                OperationResult.NotFound => NotFound(new FailureOperationStatus
                    { Code = "TradeNotFound", Message = "No exchange was found for this id" }),
                _ => StatusCode(500,
                    new FailureOperationStatus
                        { Code = "InternalServerError", Message = "An unexpected error occurred" })
            };
        }
        catch(ArgumentNullException ex)
        {
            return StatusCode(500, new FailureOperationStatus
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }

    /// <summary>
    ///     Обновление существующего трейда
    /// </summary>
    /// <param name="trade">Детали трейда для обновления</param>
    /// <returns>Обновленный трейд</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DTOTrade trade)
    {
        try
        {
            var googleId = await authorizationContext.GetGoogleIdFromAccessToken();
            if (string.IsNullOrEmpty(googleId))
            {
                return Unauthorized(new FailureOperationStatus
                {
                    Code = "Unauthorized",
                    Message = "Invalid or missing access token"
                });
            }
        
            var existingTrade = await tradeService.Get(trade.Id);
            if (existingTrade == null)
            {
                return NotFound(new FailureOperationStatus
                {
                    Code = "TradeNotFound",
                    Message = "No trade found with the given ID"
                });
            }

            if (existingTrade.UserId != googleId)
            {
                return Unauthorized(new FailureOperationStatus
                {
                    Code = "Unauthorized",
                    Message = "You do not have permission to update this trade"
                });
            }
        
            var applicationTrade = mapper.Map<ApplicationTrade>(trade);
            applicationTrade.UserId = googleId;
            var result = await tradeService.Update(applicationTrade);

            return result switch
            {
                OperationResult.Success => Ok(new SuccessOperationStatus<object> 
                { 
                    Code = "Ok", 
                    Message = "Trade updated successfully" 
                }),
                OperationResult.NotFound => NotFound(new FailureOperationStatus 
                { 
                    Code = "TradeNotFound", 
                    Message = "Trade or associated user not found" 
                }),
                _ => StatusCode(500, new FailureOperationStatus 
                { 
                    Code = "InternalServerError", 
                    Message = "An unexpected error occurred" 
                })
            };
        }
        catch (ArgumentNullException ex)
        {
            return StatusCode(500, new FailureOperationStatus
            {
                Code = "InternalServerError",
                Message = ex.Message
            });
        }
    }
    
    /// <summary>
    ///     Получение всех трейдов
    /// </summary>
    /// <param name="pagination">Параметры пагинации</param>
    /// <param name="location">Локация для фильтрации (опционально)</param>
    [HttpGet]
    public async Task<ActionResult<IRepositoryOperationStatus>> GetOtherUsersTrades(
        [FromQuery] Pagination pagination,
        [FromQuery] string? location = null)
    {
        var response = await tradeService.GetOtherUsersTrades(pagination, location, null);
        var trades = response.Data?.Trades.Select(t => mapper.Map<DTOTrade>(t));
        return new SuccessOperationStatus<GetTradeResponse>
        {
            Code = "Ok",
            Message = "",
            Data = new GetTradeResponse(response.Data?.Count ?? 0, trades)
        };
    }
}