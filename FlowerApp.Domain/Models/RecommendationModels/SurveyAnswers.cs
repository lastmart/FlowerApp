namespace FlowerApp.Domain.Models.RecommendationModels;

public class SurveyAnswer
{
    public int QuestionId { get; set; }
    public List<string> SelectedAnswers { get; set; } = new();
}