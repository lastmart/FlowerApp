namespace FlowerApp.Service.Services;

public interface IAuthorizationContext
{
    public Task<string> GetGoogleIdFromAccessToken();
}