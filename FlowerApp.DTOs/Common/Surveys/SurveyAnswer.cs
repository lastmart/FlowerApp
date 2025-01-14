namespace FlowerApp.DTOs.Common.Surveys;

public class SurveyAnswer
{
    public int QuestionId { get; set; }
    public List<int> QuestionsMask { get; set; }
}