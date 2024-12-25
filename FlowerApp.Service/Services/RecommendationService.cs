// using AutoMapper;
// using FlowerApp.Data.DbModels.Surveys;
// using FlowerApp.Data.Storages;
// using FlowerApp.Domain.Models.RecommendationModels;
// using FlowerApp.Service.Clients;
// using FlowerApp.Service.Storages;
// using Flower = FlowerApp.Domain.Models.FlowerModels.Flower;
// using Question = FlowerApp.Domain.Models.RecommendationModels.Question;
//
// namespace FlowerApp.Service.Services;
//
// public class RecommendationService: IRecommendationService
// {
//     private readonly IQuestionsStorage questionsStorage;
//     private readonly IUserAnswersStorage userAnswersStorage;
//     private readonly IUserStorage usersStorage;
//     private readonly IMapper mapper;
//     private readonly IRecommendationSystemClient recommendationSystemClient;
//     private readonly IFlowersStorage flowersStorage;
//     
//     public RecommendationService(
//         IQuestionsStorage questionsStorage,
//         IUserAnswersStorage userAnswersStorage,
//         IUserStorage usersStorage,
//         IMapper mapper,
//         IRecommendationSystemClient recommendationSystemClient,
//         IFlowersStorage flowersStorage
//         )
//     {
//         this.questionsStorage = questionsStorage;
//         this.userAnswersStorage = userAnswersStorage;
//         this.usersStorage = usersStorage;
//         this.mapper = mapper;
//         this.recommendationSystemClient = recommendationSystemClient;
//         this.flowersStorage = flowersStorage;
//     }
//     
//     public async Task<List<Question>> GetQuestions()
//     {
//         var questions = await questionsStorage.GetAll();
//         return mapper.Map<List<Question>>(questions);
//     }
//     
//
//     public async Task<RecommendationResult> GetRecommendations(Guid? userId, string? name, List<Answer> answers, int take)
//     {
//         userId ??= (await usersStorage.CreateUser(name ?? string.Empty)).Id;
//
//         var errors = new List<string>();
//         await SynchronizeAnswersForUser(answers, userId.Value, errors);
//
//         if (errors.Any())
//         {
//             return new RecommendationResult(errors); 
//         }
//
//         var recommendations = await recommendationSystemClient.GetRecommendations(userId.Value, take);
//         var flowerList = await flowersStorage.Get(recommendations.ToArray());
//         
//         return new RecommendationResult(mapper.Map<List<Flower>>(flowerList));
//     }
//
//     private async Task SynchronizeAnswersForUser(List<Answer> answers, Guid userId, IList<string> errors)
//     {
//         var userAnswersFromDb = await userAnswersStorage.GetByUser(userId);
//         foreach (var answer in answers)
//         {   
//             var question = await questionsStorage.Get(answer.QuestionId);
//             if (question == null)
//             {
//                 continue;
//             }
//             
//             var invalidAnswers = answer.SelectedAnswers
//                 .Where(selectedAnswer => !question.AnswerOptions.Contains(selectedAnswer))
//                 .ToList();
//
//             if (invalidAnswers.Any())
//             {
//                 errors.Add($"Invalid answers for question {answer.QuestionId}: {string.Join(", ", invalidAnswers)}");
//                 continue;  
//             }
//             
//             var answerMask = GetAnswerMask(answer.SelectedAnswers, question.AnswerOptions);
//             var existingAnswer = userAnswersFromDb
//                 .FirstOrDefault(ua => ua.QuestionId == answer.QuestionId);
//             
//             if (existingAnswer != null)
//             {
//                 existingAnswer.AnswerMask = answerMask;
//                 await userAnswersStorage.Update(existingAnswer);
//             }
//             else
//             {
//                 var userAnswer = new UserAnswer
//                 {
//                     UserId = userId,
//                     QuestionId = answer.QuestionId,
//                     AnswerMask = answerMask,
//                     AnswersSize = answer.SelectedAnswers.Count
//                 };
//                 await userAnswersStorage.Create(userAnswer);
//             }
//         }
//     }
//     
//     private int GetAnswerMask(List<string> selectedAnswers, List<string> allAnswerOptions)
//     {
//         return selectedAnswers
//             .Select(t => allAnswerOptions.IndexOf(t))
//             .Where(index => index >= 0)
//             .Aggregate(0, (current, index) => current | 1 << index);
//     }
// }