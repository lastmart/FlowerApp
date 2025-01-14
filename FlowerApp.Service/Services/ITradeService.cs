using FlowerApp.Domain.Models.Operation;
using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<Trade?> Get(int id);
    Task<GetTradesResponse> GetOtherUsersTrades(Pagination pagination, string? location, int? id);
    Task<GetTradesResponse> GetUserTrades(Pagination pagination, string? location, int id);
    Task<OperationResult> Create(Trade trade);
    Task<OperationResult> Update(Trade trade);
    Task<OperationResult> DeactivateTrade(int id);
}