namespace FlowerApp.Data.DbModels.Surveys;

public class SurveyQuestion : Entity<int>
{
    public string Text { get; set; }
    public QuestionType QuestionType { get; set; }
    public string Variants { get; set; }

    public IEnumerable<SurveyAnswer> Answers { get; set; }
}