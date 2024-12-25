using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<Trade?> Get(Guid id);
    Task<GetTradesResponse> GetAll(Pagination pagination, string? location, string? userId, bool includeUserTrades);
    Task<bool> Create(Trade trade);
    Task<bool> Update(Guid id, Trade trade);
    Task<bool> DeactivateTrade(Guid id);
}