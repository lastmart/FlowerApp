using System.Security.Authentication;
using System.Web;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.ApplicationModels.AuthModels;
using FlowerApp.Service.Configuration;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util;
using Kontur.Results;
using Newtonsoft.Json;

namespace FlowerApp.Service.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly ApiSecretSettings apiSecretSettings;
    private readonly ApiSettings apiSettings;
    private readonly IUserStorage userStorage;

    public GoogleAuthService(IUserStorage userStorage)
    {
        this.userStorage = userStorage;
    }

    public async Task<Result<bool, AuthTokens>> TryAuthenticateUserAsync(string authCode)
    {
        try
        {
            var authTokens = await GetUserCredentials(authCode);
            // if (await userStorage.GetUserByAccessToken(authTokens.AccessToken) != null) return authTokens;
            var userInfo = GetUserInfoByAccessToken(authTokens.AccessToken);
            // await userStorage.Create(new User {})
            return authTokens;
        }
        catch (TokenResponseException ex)
        {
            return false;
        }
        catch (Exception ex)
        {
            throw new AuthenticationException();
        }
    }

    private async Task<AuthTokens> GetUserCredentials(string authCode)
    {
        var tokenRequest = new AuthorizationCodeTokenRequest
        {
            ClientId = apiSecretSettings.GoogleClientId,
            ClientSecret = apiSecretSettings.GoogleClientSecret,
            Code = authCode,
            RedirectUri = apiSettings.RedirectedUrl
        };
        var tokenResponse = await tokenRequest.ExecuteAsync(new HttpClient(), apiSettings.GoogleAuthUrl,
            CancellationToken.None, SystemClock.Default);

        return new AuthTokens(tokenResponse.AccessToken);
    }

    private async Task<Dictionary<string, string>> GetUserInfoByAccessToken(string accessToken)
    {
        using var httpClient = new HttpClient();
        var userInfoUri = new UriBuilder(apiSettings.GoogleUserInfoUrl);
        var query = HttpUtility.ParseQueryString(userInfoUri.Query);
        query["accessToken"] = accessToken;
        userInfoUri.Query = query.ToString();

        var response = await httpClient.GetAsync(userInfoUri.ToString());
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Ошибка при получении информации о пользователе: {responseContent}");

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent)!;
    }
}