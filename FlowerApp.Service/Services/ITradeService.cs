using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<Trade?> Get(int id);
    Task<GetTradesResponse> GetOtherUsersTrades(Pagination pagination, string? location, int? id);
    Task<GetTradesResponse> GetUserTrades(Pagination pagination, string? location, int id);
    Task<bool> Create(Trade trade);
    Task<bool> Update(Trade trade);
    Task<bool> DeactivateTrade(int id);
}