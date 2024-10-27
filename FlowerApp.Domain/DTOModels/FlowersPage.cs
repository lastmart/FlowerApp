namespace FlowerApp.Domain.DTOModels;

public record FlowersPage<T>(int Count, IEnumerable<T> Flowers);