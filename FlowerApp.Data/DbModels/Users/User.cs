namespace FlowerApp.Data.DbModels.Users;

public record User : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
    
    public User()
    {
        Id = Guid.NewGuid(); 
    }
}