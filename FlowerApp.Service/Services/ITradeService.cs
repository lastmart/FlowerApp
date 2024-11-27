using FlowerApp.Domain.ApplicationModels.FlowerModels;
using DbTrade = FlowerApp.Domain.DbModels.Trade; 
using FlowerApp.Domain.Common;
using ApplicationTrade = FlowerApp.Domain.ApplicationModels.TradeModels.Trade;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<DbTrade?> Get(Guid id);
    Task<GetTradeResponse> GetAll(Pagination pagination, string? location, string? userId, bool includeUserTrades);
    Task<DbTrade?> Create(ApplicationTrade trade);
    Task<DbTrade?> Update(Guid id, ApplicationTrade trade);
}