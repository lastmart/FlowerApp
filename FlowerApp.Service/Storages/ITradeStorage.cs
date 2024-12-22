using FlowerApp.Domain.Common;
using FlowerApp.Domain.Models.TradeModels;

namespace FlowerApp.Service.Storages;

public interface ITradeStorage
{
    Task<Trade?> Get(Guid id);
    Task<IEnumerable<Trade>> GetAll(Pagination pagination, string? location, string? userId, bool excludeUserTrades);
    Task<bool> Create(Trade trade);
    Task<bool> Update(Guid id, Trade trade);
    Task<bool> DeactivateTrade(Guid id);
}