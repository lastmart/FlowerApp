using DbTrade = FlowerApp.Domain.DbModels.Trade; 
using FlowerApp.Domain.Common;
using ApplicationTrade = FlowerApp.Domain.ApplicationModels.FlowerModels.Trade;

namespace FlowerApp.Service.Services;

public interface ITradeService
{
    Task<DbTrade?> Get(Guid id);
    Task<IEnumerable<DbTrade>> GetAll(Pagination pagination, string? location, string? userId, bool excludeUserTrades);
    Task<DbTrade?> Create(ApplicationTrade trade);
    Task<DbTrade?> Update(Guid id, ApplicationTrade trade);
}