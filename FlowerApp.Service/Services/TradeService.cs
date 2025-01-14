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
        
        public async Task<TradeResponse> GetOtherUsersTrades(Pagination pagination, string? location, string? userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await userStorage.Get(userId);
                if (user == null)
                {
                    return new TradeResponse(OperationResult.NotFound, "User not found", null);
                }
            }

            var trades = (await tradeStorage.GetOtherUsersTrades(pagination, location, userId)).ToList();
            return new TradeResponse(OperationResult.Success, "Trades fetched successfully", new GetTradesResponse(trades.Count, trades));
        }
        
        public async Task<TradeResponse> GetUserTrades(Pagination pagination, string? location, string userId)
        {
            var user = await userStorage.Get(userId);
            if (user == null)
            {
                return new TradeResponse(OperationResult.NotFound, "User not found", null);
            }
            var trades = (await tradeStorage.GetUserTrades(pagination, location, userId)).ToList();
            return new TradeResponse(OperationResult.Success, "User trades fetched successfully", new GetTradesResponse(trades.Count, trades));
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
        
        public async Task<TradeResponse> GetAllTrades(Pagination pagination, string? location = null)
        {
            var trades = (await tradeStorage.GetAllTrades(pagination, location)).ToList();
            return new TradeResponse(OperationResult.Success, "Trades fetched successfully", new GetTradesResponse(trades.Count, trades));
        }
    }