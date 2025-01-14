namespace FlowerApp.Domain.Models.RecommendationModels;

public class SurveyAnswer
{
    public int QuestionId { get; set; }
    public List<int> QuestionMask { get; set; } = new();
}