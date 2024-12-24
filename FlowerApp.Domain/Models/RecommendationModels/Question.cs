namespace FlowerApp.Domain.Models.RecommendationModels;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<string> AnswerOptions { get; set; } = new();
}