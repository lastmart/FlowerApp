namespace FlowerApp.Domain.ApplicationModels.RecommendationsModels;

public class RecommendationsResponse
{
    public int Count { get; set; }
    public List<int> FlowerIds { get; set; }
}