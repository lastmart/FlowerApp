using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Storages;

public interface ITradeStorage : IStorage<Trade, int>
{
    Task<IList<Trade>> GetOtherUsersTrades(Pagination pagination, string? location, string? excludeUserId);
    Task<IList<Trade>> GetUserTrades(Pagination pagination, string? location, string userId);
    Task<bool> DeactivateTrade(int id);
}