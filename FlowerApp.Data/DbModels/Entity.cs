namespace FlowerApp.Data.DbModels;

/// <summary>
///     Base class of all Data Base Entities
/// </summary>
public class Entity<TId>
{
    public TId Id { get; init; }
}