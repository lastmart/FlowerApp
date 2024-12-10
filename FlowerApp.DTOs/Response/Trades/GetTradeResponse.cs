using FlowerApp.DTOs.Common.Trades;

namespace FlowerApp.DTOs.Response.Trades;

public record GetTradeResponse(int Count, IEnumerable<Trade> Trades);