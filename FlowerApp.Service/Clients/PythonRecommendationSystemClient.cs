using System.Text.Json;
using FlowerApp.Domain.ApplicationModels.RecommendationsModels;
using FlowerApp.Service.Configuration;

namespace FlowerApp.Service.Clients;

public class PythonRecommendationSystemClient : IRecommendationSystemClient
{
    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public PythonRecommendationSystemClient(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        this.configuration = configuration;
    }
    
    public async Task<List<int>> GetRecommendations(Guid userId, int take)
    {
        var request = new
        {
            user_id = userId,
            take = take
        };
        
        var requestContent = new StringContent(
            JsonSerializer.Serialize(request), 
            System.Text.Encoding.UTF8, 
            "application/json"
        );

        var connectionUrl = configuration.GetSection("ApiSettings").Get<ApiSettings>().RecommendationSystemUrl;
        var response = await httpClient.PostAsync(connectionUrl, requestContent);
        var result = await response.Content.ReadFromJsonAsync<RecommendationsResponse>();

        return response.IsSuccessStatusCode ? result?.FlowerIds ?? new List<int>() : new List<int>();
    }
}