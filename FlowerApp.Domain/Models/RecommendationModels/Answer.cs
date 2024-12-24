namespace FlowerApp.Domain.Models.RecommendationModels;

public class Answer
{
    public int QuestionId { get; set; }
    public List<string> SelectedAnswers { get; set; } = new();
}