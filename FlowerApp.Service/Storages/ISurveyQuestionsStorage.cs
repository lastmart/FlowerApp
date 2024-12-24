using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.Service.Storages;

public interface ISurveyQuestionsStorage: IStorage<SurveyQuestion, int>
{
    Task<IList<SurveyQuestion>> GetAll();
}