using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.ApplicationModels.RecommendationsModels;

namespace FlowerApp.Service.Services;

public interface IRecommendationService
{
    Task<List<Question>> GetQuestions();
    Task<RecommendationResult> GetRecommendations(Guid? userId, string? name, int take, List<Answer> userAnswers);
}

