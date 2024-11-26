namespace FlowerApp.Domain.ApplicationModels.RecommendationsModels;

public class Answer
{
    public int QuestionId { get; set; }
    public List<string> SelectedAnswers { get; set; } = new();
}