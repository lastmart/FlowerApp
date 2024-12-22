namespace FlowerApp.Domain.Models.FlowerModels;

public record GetFlowerResponse(int Count, IEnumerable<Flower> Flowers);