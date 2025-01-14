namespace FlowerApp.DTOs.Common.Surveys;

public class Survey
{
    public string UserId { get; set; }
    public List<SurveyAnswer> Answers { get; set; } = new();
}