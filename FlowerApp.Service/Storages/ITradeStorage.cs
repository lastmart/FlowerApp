using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Storages;

public interface ITradeStorage : IStorage<Trade, int>
{
    Task<IList<Trade>> GetOtherUsersTrades(Pagination pagination, string? location, int? excludeUserId);
    Task<IList<Trade>> GetUserTrades(Pagination pagination, string? location, int id);
    Task<bool> DeactivateTrade(int id);
}