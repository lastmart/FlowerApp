using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.Domain.Common;
using FlowerApp.Service.Storages;

namespace FlowerApp.Service.Services;

public class TradeService : ITradeService
{
    private readonly ITradeStorage tradeStorage;
    private readonly IUserStorage userStorage;

    public TradeService(ITradeStorage tradeStorage, IUserStorage userStorage)
    {
        this.tradeStorage = tradeStorage;
        this.userStorage = userStorage;
    }

    public async Task<Trade?> Get(Guid id)
    {
        var trade = await tradeStorage.Get(id);
        return trade;
    }

    public async Task<GetTradesResponse> GetAll(Pagination pagination, string? location, string? userId,
        bool includeUserTrades)
    {
        var trades = (await tradeStorage.GetAll(pagination, location, userId, includeUserTrades)).ToList();

        return new GetTradesResponse(trades.Count, trades);
    }

    public async Task<bool> Create(Trade trade)
    {
        var user = await userStorage.Get(trade.UserIdentifier);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Telegram))
        {
            throw new InvalidOperationException("User must have either email or telegram specified");
        }

        return await tradeStorage.Create(trade);
    }

    public async Task<bool> Update(Guid id, Trade trade)
    {
        var dbTrade = await tradeStorage.Get(id);
        if (dbTrade == null)
        {
            return false;
        }

        return await tradeStorage.Update(id, trade);
    }

    public async Task<bool> DeactivateTrade(Guid id)
    {
        var trade = await tradeStorage.Get(id);
        if (trade == null)
        {
            return false;
        }

        return await tradeStorage.DeactivateTrade(id);
    }
}