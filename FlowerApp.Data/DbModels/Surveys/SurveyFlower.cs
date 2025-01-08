using FlowerApp.Data.DbModels.Flowers;

namespace FlowerApp.Data.DbModels.Surveys;

/// <summary>
/// A model for a recommendation system that represents a relationship flower with probability for each answer
/// </summary>
public class SurveyFlower : Entity<int>
{
    public string RelevantVariantsProbabilities { get; set; }
    public int FlowerId { get; set; }
    public int SurveyQuestionId { get; set; }

    public Flower Flower { get; set; }
    public SurveyQuestion SurveyQuestion { get; set; }
}