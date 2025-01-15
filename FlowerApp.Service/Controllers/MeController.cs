using AutoMapper;
using FlowerApp.Domain.Models.UserModels;
using FlowerApp.DTOs.Common.Surveys;
using FlowerApp.Service.Services;
using FlowerApp.Service.Storages.SurveyStorages;
using Microsoft.AspNetCore.Mvc;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/me")]
public class MeController : ControllerBase
{
    private readonly IAuthorizationContext authorizationContext;
    private readonly IUserService userService;
    private readonly ISurveyStorage surveyStorage;
    private readonly IMapper mapper;

    public MeController(
        IAuthorizationContext authorizationContext,
        IUserService userService,
        ISurveyStorage surveyStorage,
        IMapper mapper
    )
    {
        this.authorizationContext = authorizationContext;
        this.userService = userService;
        this.surveyStorage = surveyStorage;
        this.mapper = mapper;
    }

    [HttpGet("info")]
    public async Task<ActionResult<User>> GetUserInfo()
    {
        var userId = await authorizationContext.GetGoogleIdFromAccessToken();
        var user = await userService.Get(userId);
        return Ok(user);
    }

    [HttpGet("survey-info")]
    public async Task<ActionResult<Survey>> GetSurveyInfo()
    {
        var userId = await authorizationContext.GetGoogleIdFromAccessToken();
        var survey = await surveyStorage.GetByUser(userId);
        return Ok(mapper.Map<Survey>(survey));
    }
}