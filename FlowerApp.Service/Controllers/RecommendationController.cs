using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.ApplicationModels.RecommendationsModels;
using FlowerApp.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/recommendation")]
public class RecommendationController: ControllerBase
{
    private readonly IRecommendationService recommendationService;
    
    public RecommendationController(IRecommendationService recommendationService)
    {
        this.recommendationService = recommendationService;
    }
    
    /// <summary>
    /// Получить вопросы для рекомендаций
    /// </summary>
    /// <returns>Список вопросов</returns>
    [HttpGet("questions")]
    public async Task<ActionResult<List<Question>>> GetQuestions()
    {
        var questions = await recommendationService.GetQuestions();
        return Ok(questions);
    }
    
    /// <summary>
    /// Получить рекомендации по цветам на основе ответов пользователя
    /// </summary>
    /// <remarks>
    /// Передайте answers для получения рекомендованных цветов.
    /// </remarks>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="name">Имя пользователя (обязательно, если userId не задан)</param>
    /// <param name="answers">Ответы пользователя на вопросы</param>
    /// <param name="take">Количество рекомендаций (по умолчанию 5)</param>
    /// <returns>Список рекомендованных цветов</returns>
    [HttpPost("getRecommendations")]
    public async Task<ActionResult<List<Flower>>> GetRecommendations(
        [FromQuery] Guid? userId,
        [FromQuery] string? name,
        [FromBody] List<Answer> answers,
        [FromQuery] int take = 5
    )
    {
        var result = await recommendationService.GetRecommendations(userId, name, take, answers);
    
        if (result.HasErrors)
        {
            return BadRequest(new { Errors = result.Errors });
        }

        return Ok(result.Flowers);
    }
}