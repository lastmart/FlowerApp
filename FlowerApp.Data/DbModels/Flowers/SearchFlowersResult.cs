namespace FlowerApp.Data.DbModels.Flowers;

public record SearchFlowersResult<T>(int Count, IEnumerable<T> Flowers);