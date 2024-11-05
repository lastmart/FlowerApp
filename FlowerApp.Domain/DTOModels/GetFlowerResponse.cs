namespace FlowerApp.Domain.DTOModels;

public record GetFlowerResponse(int Count, IEnumerable<FlowerDto> Flowers);