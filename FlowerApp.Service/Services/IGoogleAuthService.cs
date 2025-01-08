using FlowerApp.Domain.Models.AuthModels;
using FlowerApp.Domain.Models.UserModels;
using Kontur.Results;

namespace FlowerApp.Service.Services;

public interface IGoogleAuthService
{
    public Task<Result<bool, AuthTokens>> TryAuthenticateUserAsync(string authCode);
    public Task<Result<bool, User>> GetUserByAccessToken(string accessToken);
}