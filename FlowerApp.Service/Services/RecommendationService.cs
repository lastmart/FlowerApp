using System.Text.Json;
using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.ApplicationModels.RecommendationsModels;
using FlowerApp.Domain.DbModels;
using Flower = FlowerApp.Domain.ApplicationModels.FlowerModels.Flower;
using Question = FlowerApp.Domain.ApplicationModels.RecommendationsModels.Question;

namespace FlowerApp.Service.Services;

public class RecommendationService: IRecommendationService
{
    private readonly IQuestionsStorage questionsStorage;
    private readonly IUserAnswersStorage userAnswersStorage;
    private readonly IUserStorage usersStorage;
    private readonly IMapper mapper;
    private readonly HttpClient httpClient;
    private readonly IFlowersStorage flowersStorage;
    
    public RecommendationService(
        IQuestionsStorage questionsStorage,
        IUserAnswersStorage userAnswersStorage,
        IUserStorage usersStorage,
        IMapper mapper,
        HttpClient httpClient,
        IFlowersStorage flowersStorage
        )
    {
        this.questionsStorage = questionsStorage;
        this.userAnswersStorage = userAnswersStorage;
        this.usersStorage = usersStorage;
        this.mapper = mapper;
        this.httpClient = httpClient;
        this.flowersStorage = flowersStorage;
    }
    
    public async Task<List<Question>> GetQuestions()
    {
        var questions = await questionsStorage.GetAll();
        return mapper.Map<List<Question>>(questions);
    }
    

    public async Task<RecommendationResult> GetRecommendations(Guid? userId, string? name, int take, List<Answer> answers)
    {
        if (userId == null)
        {
            var user = await usersStorage.CreateUser(name ?? string.Empty);
            userId = user.Id;
        }
    
        var errors = new List<string>();
        
        var existingQuestionIds = (await questionsStorage.GetAll()).Select(q => q.Id).ToHashSet();
        
        var invalidQuestionIds = answers
            .Where(answer => !existingQuestionIds.Contains(answer.QuestionId))
            .Select(answer => answer.QuestionId)
            .Distinct()
            .ToList();
        
        if (invalidQuestionIds.Any())
        {
            errors.Add($"Invalid question IDs: {string.Join(", ", invalidQuestionIds)}");
            return new RecommendationResult(errors);
        }
        
        var userAnswersFromDb = await userAnswersStorage.GetByUser(userId.Value);
        
        foreach (var answer in answers)
        {   
            var question = await questionsStorage.Get(answer.QuestionId);
            if (question == null)
            {
                continue;
            }
            
            var invalidAnswers = answer.SelectedAnswers
                .Where(selectedAnswer => !question.AnswerOptions.Contains(selectedAnswer))
                .ToList();

            if (invalidAnswers.Any())
            {
                errors.Add($"Invalid answers for question {answer.QuestionId}: {string.Join(", ", invalidAnswers)}");
                continue;  
            }
            
            var answerMask = GetAnswerMask(answer.SelectedAnswers, question.AnswerOptions);
            
            var existingAnswer = userAnswersFromDb
                .FirstOrDefault(ua => ua.QuestionId == answer.QuestionId);
            
            if (existingAnswer != null)
            {
                existingAnswer.AnswerMask = answerMask;
                await userAnswersStorage.Update(existingAnswer);
            }
            else
            {
                var userAnswer = new UserAnswer
                {
                    UserId = userId.Value,
                    QuestionId = answer.QuestionId,
                    AnswerMask = answerMask,
                    AnswersSize = answer.SelectedAnswers.Count
                };
                await userAnswersStorage.Create(userAnswer);
            }
        }
        
        if (errors.Any())
        {
            return new RecommendationResult(errors); 
        }
        
        var recommendations = await GetRecommendationsFromPython(userId.Value, take);
        var flowerList = await flowersStorage.Get(recommendations.ToArray());
        
        return new RecommendationResult(mapper.Map<List<Flower>>(flowerList));
    }
    
    private async Task<List<int>> GetRecommendationsFromPython(Guid userId, int take)
    {
        var request = new
        {
            user_id = userId,
            take = take
        };
        
        var requestContent = new StringContent(
            JsonSerializer.Serialize(request), 
            System.Text.Encoding.UTF8, 
            "application/json"
        );
        
        var response = await httpClient.PostAsync("http://127.0.0.1:8000/recommendations", requestContent);
        var result = await response.Content.ReadFromJsonAsync<RecommendationsResponse>();
        
        return response.IsSuccessStatusCode ? result?.FlowerIds ?? new List<int>() : new List<int>();
    }
    
    private int GetAnswerMask(List<string> selectedAnswers, List<string> allAnswerOptions)
    {
        return selectedAnswers
            .Select(t => allAnswerOptions.IndexOf(t))
            .Where(index => index >= 0)
            .Aggregate(0, (current, index) => current | 1 << index);
    }
}