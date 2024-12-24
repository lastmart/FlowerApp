namespace FlowerApp.Domain.Models.RecommendationModels;

public class RecommendationsResponse
{
    public int Count { get; set; }
    public List<int> FlowerIds { get; set; }
}