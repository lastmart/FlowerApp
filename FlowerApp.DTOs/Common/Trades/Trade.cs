namespace FlowerApp.DTOs.Common.Trades;

public record Trade
{
    public int Id { get; set; }
    public string FlowerName { get; set; }
    public string PreferredTrade { get; set; }
    public string Location { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public string PhotoBase64 { get; set; }
    public string AuthorName { get; set; }
    public string ContactData { get; set; }
}