namespace FlowerApp.Domain.DbModels;

public class LightParameters : Entity<int>
{
    public double IlluminationInSuites { get; init; }
    public int DurationInHours { get; init; }

    public IEnumerable<Flower> Flowers;
}