using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Storages;

public interface ITradeStorage : IStorage<Trade, int>
{
    Task<IEnumerable<Trade>> Get(Pagination pagination, string? location, string? excludeId = null);
    Task<bool> DeactivateTrade(int id);
}