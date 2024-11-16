namespace FlowerApp.Domain.DbModels;

public record SearchFlowersResult<T>(int Count, IEnumerable<T> Flowers);