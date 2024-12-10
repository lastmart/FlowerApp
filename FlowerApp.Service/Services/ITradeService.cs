using FlowerApp.Domain.ApplicationModels.TradeModels;
using FlowerApp.Domain.Common;
using FlowerApp.DTOs.Response.Trades;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<Trade?> Get(Guid id);
    Task<GetTradeResponse> GetAll(Pagination pagination, string? location, string? userId, bool includeUserTrades);
    Task<bool> Create(Trade trade);
    Task<bool> Update(Guid id, Trade trade);
    Task<bool> DeactivateTrade(Guid id);
}