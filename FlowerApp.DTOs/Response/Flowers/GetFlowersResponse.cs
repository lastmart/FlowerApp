using FlowerApp.DTOs.Common.Flowers;

namespace FlowerApp.DTOs.Response.Flowers;

public record GetFlowerResponse(int Count, IEnumerable<Flower> Flowers);