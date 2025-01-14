using FlowerApp.Domain.Models.Operation;
using FlowerApp.Domain.Models.TradeModels;
using FlowerApp.DTOs.Common;
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
        
        public async Task<Trade?> Get(int id)
        {
            var trade = await tradeStorage.Get(id);
            return trade;
        }
        
        public async Task<GetTradesResponse?> GetOtherUsersTrades(Pagination pagination, string? location, int? userId)
        {
            if (userId.HasValue)
            {
                var user = await userStorage.Get(userId.Value);
                if (user == null)
                {
                    return null;
                }
            }

            var trades = (await tradeStorage.GetOtherUsersTrades(pagination, location, userId)).ToList();
            return new GetTradesResponse(trades.Count, trades);
        }
        
        public async Task<GetTradesResponse?> GetUserTrades(Pagination pagination, string? location, int userId)
        {
            var user = await userStorage.Get(userId);
            if (user == null)
            {
                return null;
            }
            var trades = (await tradeStorage.GetUserTrades(pagination, location, userId)).ToList();
            return new GetTradesResponse(trades.Count, trades);
        }
        
        public async Task<OperationResult> Create(Trade trade)
        {
            var user = await userStorage.Get(trade.UserId);
            if (user == null)
            {
                return OperationResult.NotFound;
            }
            
            if (string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.Telegram))
            {
                return OperationResult.InvalidData;
            }

            var isSuccess = await tradeStorage.Create(trade);
            return isSuccess ? OperationResult.Success : OperationResult.Conflict;
        }
        
        public async Task<OperationResult> Update(Trade trade)
        {
            var dbTrade = await tradeStorage.Get(trade.Id);
            if (dbTrade == null)
            {
                return OperationResult.NotFound;
            }
            
            var user = await userStorage.Get(trade.UserId);
            if (user == null)
            {
                return OperationResult.NotFound;
            }
            
            var isSuccess = await tradeStorage.Update(trade);
            return isSuccess ? OperationResult.Success : OperationResult.Conflict;
        }
        
        public async Task<OperationResult> DeactivateTrade(int id)
        {
            var trade = await tradeStorage.Get(id);
            if (trade == null)
            {
                return OperationResult.NotFound;
            }
            
            var isSuccess = await tradeStorage.DeactivateTrade(id);
            return isSuccess ? OperationResult.Success : OperationResult.Conflict;
        }
    }