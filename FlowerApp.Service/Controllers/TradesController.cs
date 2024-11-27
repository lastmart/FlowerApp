using FlowerApp.Domain.ApplicationModels.FlowerModels;
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

    [HttpGet]
    public async Task<ActionResult<IList<Trade>>> GetTrades(
        [FromQuery] Pagination pagination,
        [FromQuery] string? location,
        [FromQuery] string? userId,
        [FromQuery] bool excludeUserTrades = false)
    {
        var trades = await tradeService.GetAll(pagination, location, userId, excludeUserTrades);
        return Ok(trades);
    }

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