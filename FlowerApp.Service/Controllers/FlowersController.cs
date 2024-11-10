using AutoMapper;
using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.Common;
using FlowerApp.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/flowers")]
public class FlowersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IFlowersService flowersService;
    private readonly IValidator<Pagination> paginationValidator;

    public FlowersController(
        IFlowersService flowersService,
        IMapper mapper,
        IValidator<Pagination> paginationValidator
    )
    {
        this.flowersService = flowersService;
        this.mapper = mapper;
        this.paginationValidator = paginationValidator;
    }

    [SwaggerOperation(
        Summary = "Получение цветов",
        Description =
            "Возвращает постраничный список цветов с основной информацией о каждом цветке и параметрами освещения",
        OperationId = "GetFlowers"
    )]
    [HttpGet]
    public async Task<ActionResult<GetFlowerResponse>> Get(
        [FromQuery] Pagination pagination,
        [FromQuery] FlowerFilterParams filterParams,
        [FromQuery] FlowerSortOptions sortOption
    )
    {
        var paginationValidationResult = await paginationValidator.ValidateAsync(pagination);
        if (!paginationValidationResult.IsValid)
            return BadRequest(paginationValidationResult.Errors);

        var getFlowerResponse = await flowersService.Get(pagination, filterParams, sortOption);
        return Ok(getFlowerResponse);
    }

    [SwaggerOperation(
        Summary = "Получить цветок по идентификатору",
        Description = "Возвращает детальную информацию о конкретном цветке по его ID, включая параметры освещения",
        OperationId = "GetFlowerById"
    )]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var flower = await flowersService.Get(id);
        return flower != null ? Ok(flower) : NotFound();
    }

    [SwaggerOperation(
        Summary = "Получить цветок по названию",
        Description = "Возвращает детальную информацию о цветке по его названию",
        OperationId = "GetFlowerByName"
    )]
    [HttpGet("{name}")]
    public async Task<IActionResult> Get(string name)
    {
        var flower = await flowersService.Get(name);
        return flower != null ? Ok(flower) : NotFound();
    }
}