
using FlowerApp.Data.DbModels.Users;

namespace FlowerApp.Data.DbModels.Trades;

public class Trade : Entity<int>
{
    public int UserId { get; set; }
    public string FlowerName { get; set; }
    public string PreferredTrade { get; set; }
    public string Location { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string Description { get; set; }
    
    public User User { get; set; }
}