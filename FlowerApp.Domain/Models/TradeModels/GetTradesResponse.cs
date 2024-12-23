namespace FlowerApp.Domain.Models.TradeModels;

public record GetTradesResponse(int Count, IEnumerable<Trade> Trades);