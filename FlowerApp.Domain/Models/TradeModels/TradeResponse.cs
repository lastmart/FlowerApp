using FlowerApp.Domain.Models.Operation;

namespace FlowerApp.Domain.Models.TradeModels;

public record TradeResponse(OperationResult Result, string? Message, GetTradesResponse? Data);