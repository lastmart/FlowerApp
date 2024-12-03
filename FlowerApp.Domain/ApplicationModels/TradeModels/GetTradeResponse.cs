using DbTrade = FlowerApp.Domain.DbModels.Trade; 
namespace FlowerApp.Domain.ApplicationModels.FlowerModels;

public record GetTradeResponse(int Count, IEnumerable<DbTrade> Trades);