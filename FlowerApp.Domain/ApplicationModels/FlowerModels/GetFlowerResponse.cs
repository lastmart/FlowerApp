namespace FlowerApp.Domain.ApplicationModels.FlowerModels;

public record GetFlowerResponse(int Count, IEnumerable<Flower> Flowers);