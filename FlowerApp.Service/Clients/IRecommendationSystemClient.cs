namespace FlowerApp.Service.Clients;

public interface IRecommendationSystemClient
{
    Task<List<int>> GetRecommendations(Guid userId, int take);
}