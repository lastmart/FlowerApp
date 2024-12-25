using FlowerApp.Data.DbModels.Users;

namespace FlowerApp.Data.DbModels.AuthTokens;

public class AuthTokens : Entity<int>
{
    public string AccessToken { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals((obj as AuthTokens)!);
    }

    private bool Equals(AuthTokens? other)
    {
        return other is not null && UserId == other.UserId && AccessToken == other.AccessToken;
    }
}