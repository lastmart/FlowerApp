using FlowerApp.Data.DbModels.Surveys;

namespace FlowerApp.Domain.Models.RecommendationModels;

public class SurveyQuestion : Entity<int>
{
    public string Text { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<string> Variants { get; set; }
}