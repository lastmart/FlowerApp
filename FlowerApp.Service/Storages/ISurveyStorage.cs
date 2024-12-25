using FlowerApp.Domain.Models.RecommendationModels;

namespace FlowerApp.Service.Storages;

public interface ISurveyStorage
{
    Task<bool> Create(Survey survey);
}