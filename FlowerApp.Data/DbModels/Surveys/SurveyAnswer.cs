namespace FlowerApp.Data.DbModels.Surveys;

public class SurveyAnswer : Entity<int>
{
     public string QuestionsMask { get; set; }
     public int QuestionId { get; set; }
     public int SurveyId { get; set; }

     public SurveyQuestion SurveyQuestion { get; set; }
     public Survey Survey { get; set; }
}