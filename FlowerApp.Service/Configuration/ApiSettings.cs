namespace FlowerApp.Service.Configuration;

public class ApiSettings
{
    public string RecommendationSystemUrl { get; init; } = "http://127.0.0.1:8000/recommendations";
    public string GoogleAuthUrl { get; init; } = "https://oauth2.googleapis.com/token";
    // Кароч это поле нужно, если у нас есть web версия приложения. Если появится - поймем зачем.
    // У нас такого нет, поэтому стандарт указывает проставлять пустую строку
    public string RedirectedUrl { get; init; } = "";
    public string GoogleUserInfoUrl { get; init; } = "https://www.googleapis.com/oauth2/v3/userinfo";
    public string GoogleTokenInfoUrl { get; init; } = "https://oauth2.googleapis.com/tokeninfo";
}