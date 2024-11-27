using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.ApplicationModels.FlowerModels;
using DbTrade = FlowerApp.Domain.DbModels.Trade; 
using ApplicationTrade = FlowerApp.Domain.ApplicationModels.FlowerModels.Trade;
using FlowerApp.Domain.Common;

namespace FlowerApp.Service.Services;

public class TradeService : ITradeService
{
    private readonly ITradeStorage tradeStorage;
    private readonly IMapper mapper;
    private readonly IUserStorage userStorage;

    public TradeService(ITradeStorage tradeStorage, IUserStorage userStorage, IMapper mapper)
    {
        this.tradeStorage = tradeStorage;
        this.userStorage = userStorage;
        this.mapper = mapper;
    }

    public async Task<DbTrade?> Get(Guid id)
    {
        var dbTrade = await tradeStorage.Get(id);
        return dbTrade; 
    }

    public async Task<GetTradeResponse> GetAll(Pagination pagination, string? location, string? userId, bool excludeUserTrades)
    {
        var dbTrades = await tradeStorage.GetAll(pagination, location, userId, excludeUserTrades);
        
        var tradeCount = dbTrades.Count();
        
        return new GetTradeResponse(tradeCount, dbTrades);
    }

    public async Task<DbTrade?> Create(ApplicationTrade trade)
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
        var createdTrade = await tradeStorage.Create(dbTrade);

        return createdTrade ? dbTrade : null;
    }
    
    public async Task<DbTrade?> Update(Guid id, ApplicationTrade trade)
    {
        var dbTrade = await tradeStorage.Get(id);
        if (dbTrade == null)
        {
            return null;
        }
        
        mapper.Map(trade, dbTrade); 

        var isUpdated = await tradeStorage.Update(id, dbTrade); 
        return isUpdated ? dbTrade : null;
    }

}