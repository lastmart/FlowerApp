using FlowerApp.Domain.Common;
using FlowerApp.Domain.Models.TradeModels;

namespace FlowerApp.Service.Storages;

public interface ITradeStorage : IStorage<Trade, int>
{
    Task<IEnumerable<Trade>> Get(Pagination pagination, string? location, int? excludeId = null);
    Task<bool> DeactivateTrade(int id);
}