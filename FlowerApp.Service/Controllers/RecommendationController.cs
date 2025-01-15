using AutoMapper;
using FlowerApp.DTOs.Common.Surveys;
using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;
using DtoFlower = FlowerApp.DTOs.Common.Flowers.Flower;
using AppSurvey = FlowerApp.Domain.Models.RecommendationModels.Survey;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/recommendation")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService recommendationService;
    private readonly IAuthorizationContext authorizationContext;
    private readonly IMapper mapper;

    public RecommendationController(
        IRecommendationService recommendationService,
        IAuthorizationContext authorizationContext,
        IMapper mapper
    )
    {
        this.recommendationService = recommendationService;
        this.authorizationContext = authorizationContext;
        this.mapper = mapper;
    }

    /// <summary>
    /// Получить вопросы для рекомендаций
    /// </summary>
    /// <returns>Список вопросов</returns>
    [HttpGet("questions")]
    public async Task<ActionResult<List<SurveyQuestion>>> GetQuestions()
    {
        var questions = await recommendationService.GetQuestions();
        return Ok(questions);
    }

    /// <summary>
    /// Сохранить результаты опрооса пользователя
    /// </summary>
    /// <param name="survey">Результаты опроса пользователя</param>
    /// <returns></returns>
    [HttpPost("survey")]
    public async Task<IActionResult> StoreAnswers([FromBody] Survey survey)
    {
        await recommendationService.StoreUserAnswers(mapper.Map<AppSurvey>(survey));
        return Ok();
    }

    /// <summary>
    /// Получить рекомендации по цветам на основе ответов пользователя
    /// </summary>
    /// <remarks>
    /// Передайте answers для получения рекомендованных цветов.
    /// </remarks>
    /// <param name="take">Количество рекомендаций</param>
    /// <returns>Список рекомендованных цветов</returns>
    [HttpGet]
    public async Task<ActionResult<List<DtoFlower>>> GetRecommendations([FromQuery] int take)
    {
        var userId = await authorizationContext.GetGoogleIdFromAccessToken();
        var result = await recommendationService.GetRecommendations(userId, take);

        if (result.HasErrors)
        {
            return BadRequest(new { Errors = result.Errors });
        }

        return Ok(result.Flowers);
    }
}