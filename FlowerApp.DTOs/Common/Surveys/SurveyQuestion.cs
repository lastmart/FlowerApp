using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.DTOs.Common.Surveys;

public class SurveyQuestion
{
    public int Id { get; set; }
    public string Text { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<string> Variants { get; set; }
}