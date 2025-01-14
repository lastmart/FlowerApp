using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.Service.Storages.SurveyStorages;

public interface ISurveyQuestionsStorage : IStorage<SurveyQuestion, int>
{
    Task<IList<SurveyQuestion>> GetAll();
}