namespace FlowerApp.Data.DbModels;

/// <summary>
///     Base class of all Data Base Entities
/// </summary>
public record Entity<TId>
{
    public TId Id { get; init; }
}