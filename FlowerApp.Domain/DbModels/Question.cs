namespace FlowerApp.Domain.DbModels;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int AnswerSize { get; set; }
    public List<string> AnswerOptions { get; set; } = new();
}