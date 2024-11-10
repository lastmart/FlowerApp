namespace FlowerApp.Domain.DbModels;

/// <summary>
/// Base class of all Data Base Entities
/// </summary>
public record Entity<TId>
{
    public TId Id { get; init; } 
    
    public override string ToString() => $"{GetType().Name}({nameof(Id)}: {Id})";
}