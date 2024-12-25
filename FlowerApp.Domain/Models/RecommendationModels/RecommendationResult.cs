using FlowerApp.Domain.Models.FlowerModels;

namespace FlowerApp.Domain.Models.RecommendationModels;

public class RecommendationResult
{
    public List<Flower>? Flowers { get; set; }
    public List<string>? Errors { get; set; }
    
    public bool HasErrors => Errors != null && Errors.Any();
    
    public RecommendationResult(List<Flower> flowers)
    {
        Flowers = flowers;
    }

    public RecommendationResult(List<string> errors)
    {
        Errors = errors;
    }
}