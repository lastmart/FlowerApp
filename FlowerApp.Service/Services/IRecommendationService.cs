using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.Service.Services;

public interface IRecommendationService
{
    Task<IList<SurveyQuestion>> GetQuestions();
    Task<bool> StoreUserAnswers(Survey survey);
    Task<RecommendationResult> GetRecommendations(string userId, int take);
}

