using FlowerApp.Data.DbModels.Flowers;

namespace FlowerApp.Data.DbModels.Surveys;

public class SurveyFlower : Entity<int>
{
    public string RelevantVariants { get; set; }
    public int FlowerId { get; set; }
    public int SurveyQuestionId { get; set; }

    public Flower Flower { get; set; }
    public SurveyQuestion SurveyQuestion { get; set; }
}