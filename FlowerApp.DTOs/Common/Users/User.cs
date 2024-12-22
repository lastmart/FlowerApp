namespace FlowerApp.DTOs.Common.Users;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? Telegram { get; set; }
}