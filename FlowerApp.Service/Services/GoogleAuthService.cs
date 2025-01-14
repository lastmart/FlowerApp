using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Web;
using FlowerApp.Domain.Models.AuthModels;
using FlowerApp.Domain.Models.UserModels;
using FlowerApp.Service.Configuration;
using FlowerApp.Service.Storages;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util;
using Kontur.Results;
using Newtonsoft.Json;

namespace FlowerApp.Service.Services;

public class GoogleAuthService : IGoogleAuthService
{
    private const string GoogleUserIdField = "sub";
    private const string EmailField = "email";
    private const string NameField = "given_name";
    private const string SurnameField = "family_name";
    
    private readonly ApiSecretSettings apiSecretSettings;
    private readonly ApiSettings apiSettings;
    private readonly IUserStorage userStorage;

    public GoogleAuthService(IUserStorage userStorage, Func<ApiSettings> apiSettings,
        Func<ApiSecretSettings> secretSettings)
    {
        this.userStorage = userStorage;
        this.apiSettings = apiSettings.Invoke();
        apiSecretSettings = secretSettings.Invoke();
    }

    public async Task<Result<bool, AuthTokens>> TryAuthenticateUserAsync(string authCode)
    {
        try
        {
            var authTokens = await GetUserAuthTokens(authCode);
            var userResult = await GetUserByAccessToken(authTokens.AccessToken);
            if (userResult.Failure)
            {
                return authTokens;
            }

            await userStorage.Create(userResult.GetValueOrThrow());
            return authTokens;
        }
        catch (TokenResponseException)
        {
            return false;
        }
        catch (Exception)
        {
            throw new AuthenticationException();
        }
    }

    public async Task<Result<bool, User>> GetUserByAccessToken(string accessToken)
    {
        var userInfoDictionary = await GetUserInfoByAccessToken(accessToken);
        var googleUserId = userInfoDictionary[GoogleUserIdField];
        if (await userStorage.GetByGoogleId(googleUserId) == null)
        {
            return false;
        }

        return new User
        {
            GoogleId = googleUserId,
            Email = userInfoDictionary[EmailField],
            Name = userInfoDictionary[NameField],
            Surname = userInfoDictionary[SurnameField],
            Telegram = null
        };
    }

    private async Task<AuthTokens> GetUserAuthTokens(string authCode)
    {
        var clientId = Environment.GetEnvironmentVariable("GoogleClientId") ?? apiSecretSettings.GoogleClientId;
        var clientSecret = Environment.GetEnvironmentVariable("GoogleClientSecret") ?? apiSecretSettings.GoogleClientSecret;
        var tokenRequest = new AuthorizationCodeTokenRequest
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
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
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var userInfoUri = new UriBuilder(apiSettings.GoogleUserInfoUrl);

        var response = await httpClient.GetAsync(userInfoUri.ToString());
        var responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ошибка при получении информации о пользователе: {responseContent}");
        }

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent)!;
    }
}
