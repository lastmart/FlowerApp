using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.Service.Storages.SurveyStorages;

public interface ISurveyStorage : IStorage<Survey, int>
{
    Task<Survey> GetByUser(string userId);
}