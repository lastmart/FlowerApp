namespace FlowerApp.Domain.Models.RecommendationModels;

public class Survey
{
    public int UserId { get; set; }
    private List<SurveyAnswer> Answers { get; set; } = new();
}