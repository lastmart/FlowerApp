using FlowerApp.Domain.Models.Operation;
using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<Trade?> Get(int id);
    Task<TradeResponse> GetOtherUsersTrades(Pagination pagination, string? location, string? excludeUserId);
    Task<TradeResponse> GetUserTrades(Pagination pagination, string? location, string userId);
    Task<OperationResult> Create(Trade trade);
    Task<OperationResult> Update(Trade trade);
    Task<OperationResult> DeactivateTrade(int id);
}