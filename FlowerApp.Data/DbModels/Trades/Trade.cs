namespace FlowerApp.Data.DbModels.Trades;

public record Trade : Entity<Guid>
{
    public Guid UserIdentifier { get; set; }
    public string FlowerName { get; set; }
    public string PreferredTrade { get; set; }
    public string Location { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }

    public Trade()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }
}