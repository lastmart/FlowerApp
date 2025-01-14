using AutoMapper;
using FlowerApp.Domain.Models.RecommendationModels;
using FlowerApp.Service.Clients;
using FlowerApp.Service.Storages;
using FlowerApp.Service.Storages.SurveyStorages;
using Flower = FlowerApp.Domain.Models.FlowerModels.Flower;
using Question = FlowerApp.Domain.Models.RecommendationModels.SurveyQuestion;

namespace FlowerApp.Service.Services;

public class RecommendationService : IRecommendationService
{
    private readonly ISurveyQuestionsStorage questionsStorage;
    private readonly ISurveyStorage surveyStorage;
    private readonly IMapper mapper;
    private readonly IRecommendationSystemClient recommendationSystemClient;

    private readonly IFlowersStorage flowersStorage;

    public RecommendationService(
        ISurveyQuestionsStorage questionsStorage,
        IMapper mapper,
        IRecommendationSystemClient recommendationSystemClient,
        IFlowersStorage flowersStorage,
        ISurveyStorage surveyStorage
    )
    {
        this.questionsStorage = questionsStorage;
        this.mapper = mapper;
        this.recommendationSystemClient = recommendationSystemClient;
        this.flowersStorage = flowersStorage;
        this.surveyStorage = surveyStorage;
    }

    public async Task<IList<Question>> GetQuestions()
    {
        var questions = await questionsStorage.GetAll();
        return mapper.Map<List<Question>>(questions);
    }

    public async Task<bool> StoreUserAnswers(Survey survey)
    {
        return await surveyStorage.Create(survey);
    }
    
    public async Task<RecommendationResult> GetRecommendations(string userId, int take)
    {
        var recommendations = await recommendationSystemClient.GetRecommendations(userId, take);
        var flowerList = await flowersStorage.Get(recommendations.ToArray());
        return new RecommendationResult(mapper.Map<List<Flower>>(flowerList));
    }
}