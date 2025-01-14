namespace FlowerApp.Service.Clients;

public interface IRecommendationSystemClient
{
    Task<List<int>> GetRecommendations(string userId, int take);
}