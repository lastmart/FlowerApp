namespace FlowerApp.DTOs.Common.Trades;

public record Trade
{
    public Guid Id { get; set; }
    public Guid UserIdentifier { get; set; }
    public string FlowerName { get; set; }
    public string PreferredTrade { get; set; }
    public string Location { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
}