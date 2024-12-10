using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.Common;
using FlowerApp.DTOs.Response.Trades;
using DbTrade = FlowerApp.Domain.DbModels.Trade;
using ApplicationTrade = FlowerApp.Domain.ApplicationModels.TradeModels.Trade;
using DTOTrade = FlowerApp.DTOs.Common.Trades.Trade;

namespace FlowerApp.Service.Services;

public class TradeService : ITradeService
{
    private readonly IMapper mapper;
    private readonly ITradeStorage tradeStorage;
    private readonly IUserStorage userStorage;

    public TradeService(ITradeStorage tradeStorage, IUserStorage userStorage, IMapper mapper)
    {
        this.tradeStorage = tradeStorage;
        this.userStorage = userStorage;
        this.mapper = mapper;
    }

    public async Task<ApplicationTrade?> Get(Guid id)
    {
        var dbTrade = await tradeStorage.Get(id);
        return mapper.Map<ApplicationTrade>(dbTrade);
    }

    public async Task<GetTradeResponse> GetAll(Pagination pagination, string? location, string? userId,
        bool includeUserTrades)
    {
        var dbTrades = await tradeStorage.GetAll(pagination, location, userId, includeUserTrades);
        var trades = dbTrades.Select(mapper.Map<ApplicationTrade>).ToArray();

        return new GetTradeResponse(trades.Length, trades.Select(mapper.Map<DTOTrade>));
    }

    public async Task<bool> Create(ApplicationTrade trade)
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

        var dbTrade = mapper.Map<DbTrade>(trade);
        return await tradeStorage.Create(dbTrade);
    }

    public async Task<bool> Update(Guid id, ApplicationTrade trade)
    {
        var dbTrade = await tradeStorage.Get(id);
        if (dbTrade == null)
        {
            return false;
        }

        mapper.Map(trade, dbTrade);
        return await tradeStorage.Update(id, dbTrade);
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