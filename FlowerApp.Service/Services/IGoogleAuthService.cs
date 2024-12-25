using FlowerApp.Domain.ApplicationModels.AuthModels;
using Kontur.Results;

namespace FlowerApp.Service.Services;

public interface IGoogleAuthService
{
    public Task<Result<bool, AuthTokens>> TryAuthenticateUserAsync(string authCode);
}