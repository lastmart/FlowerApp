using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.Common;
using FlowerApp.Domain.DbModels;
using FlowerApp.Domain.DTOModels;
using FlowerApp.Service.Services;
using FlowerApp.Service.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlowerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFlowersService flowersService;
    private readonly IFlowersStorage flowerStorage;
    private readonly IValidator<FlowersPaginationRequest> _paginationValidator;
    private readonly IValidator<FlowerFilterDto> _filterValidator;
    

    public FlowerController(
        IFlowersStorage flowerStorage, 
        IFlowersService flowersService, 
        IMapper mapper,
        IValidator<FlowerFilterDto> filterValidator,
        IValidator<FlowersPaginationRequest> paginationValidator
        )
    {
        this.flowerStorage = flowerStorage;
        this.flowersService = flowersService;
        _mapper = mapper;
        _filterValidator = filterValidator;
        _paginationValidator = paginationValidator;
    }

    [SwaggerOperation(
        Summary = "Получить постраничный список всех цветов",
        Description =
            "Возвращает постраничный список цветов с основной информацией о каждом цветке и параметрами освещения",
        OperationId = "GetFlowers"
    )]
    [HttpGet]
    public async Task<ActionResult<GetFlowerResponse>> GetFlowers(
        [SwaggerParameter("Сколько цветов пропустить")]
        int skip = 0,
        [SwaggerParameter("Размер страницы (не более)")]
        int take = 50)
    {
        var paginationRequest = new FlowersPaginationRequest { Skip = skip, Take = take };
        var validationResult = await _paginationValidator.ValidateAsync(paginationRequest);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        
        var flowersPage = await flowersService.GetFlowers(new Pagination(skip, take));
        return Ok(flowersPage);
    }

    [SwaggerOperation(
        Summary = "Получить цветок по идентификатору",
        Description = "Возвращает детальную информацию о конкретном цветке по его ID, включая параметры освещения",
        OperationId = "GetFlowerById"
    )]
    [HttpGet("{id:int}")]
    public ActionResult<Flower> GetFlowerById(
        [SwaggerParameter("Уникальный идентификатор цветка")]
        int id)
    {
        var flower = flowerStorage.Get(id);
        if (flower == null) return NotFound();

        var flowerDto = _mapper.Map<FlowerDto>(flower);
        return Ok(flowerDto);
    }

    [SwaggerOperation(
        Summary = "Получить цветок по названию",
        Description = "Возвращает детальную информацию о цветке по его обычному названию",
        OperationId = "GetFlowerByName"
    )]
    [HttpGet("name/{name}")]
    public ActionResult<Flower> GetFlowerByName(
        [SwaggerParameter("Название цветка")] string name)
    {
        var flower = flowerStorage.GetByName(name);
        if (flower == null) return NotFound();

        var flowerDto = _mapper.Map<FlowerDto>(flower);
        return Ok(flowerDto);
    }

    [SwaggerOperation(
        Summary = "Получить цветок по научному названию",
        Description = "Возвращает детальную информацию о цветке по его научному названию",
        OperationId = "GetFlowerByScientificName"
    )]
    [HttpGet("scientificname/{scientificName}")]
    public ActionResult<FlowerDto> GetFlowerByScientificName(
        [SwaggerParameter("Научное название цветка (латинское название)")]
        string scientificName)
    {
        var flower = flowerStorage.GetByScientificName(scientificName);
        if (flower == null) return NotFound();

        var flowerDto = _mapper.Map<FlowerDto>(flower);
        return Ok(flowerDto);
    }


    [SwaggerOperation(
        Summary = "Фильтрация цветов по различным параметрам",
        Description = @"Позволяет фильтровать цветы по следующим параметрам:<br/>
        - WateringFrequency<br/>
        - ToxicCategories<br/>
        - TransplantFrequency<br/>
        - IlluminationInSuites<br/>
        - DurationInHours",
        OperationId = "FilterFlowers"
    )]
    [SwaggerRequestExample(typeof(FlowerFilterDto), typeof(FlowerFilterExample))]
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<FlowerDto>>> FilterFlowers(
        [SwaggerParameter("Параметры фильтрации цветов")] [FromBody]
        FlowerFilterDto filterDto)
    {
        var validationResult = await _filterValidator.ValidateAsync(filterDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var filter = new FlowerFilter
        {
            WateringFrequency = filterDto.WateringFrequency,
            TransplantFrequency = filterDto.TransplantFrequency,
            IlluminationInSuites = filterDto.IlluminationInSuites,
            DurationInHours = filterDto.DurationInHours,
            ToxicCategories = filterDto.ToxicCategories?.Select(Enum.Parse<ToxicCategory>).ToArray()
        };

        var filteredFlowers = await flowerStorage.FilterFlowers(filter);
        var filteredFlowersDto = _mapper.Map<IEnumerable<FlowerDto>>(filteredFlowers);

        return Ok(filteredFlowersDto);
    }


    [SwaggerOperation(
        Summary = "Сортировка цветов по различным параметрам",
        Description = @"Позволяет сортировать цветы по следующим параметрам:<br/>
        - wateringFrequency (частота полива)<br/>
        - name (название)<br/>
        - scientificName (научное название)<br/>
        - illuminationInSuites (освещенность)<br/>
        - durationInHours (длительность освещения)<br/>
        - transplantFrequency (частота пересадки)<br/>
        - isToxic (токсичность)",
        OperationId = "SortFlowers"
    )]
    [SwaggerRequestExample(typeof(FlowerSortOptions), typeof(FlowerSortOptionsExample))]
    [HttpPost("sort")]
    public async Task<ActionResult<IEnumerable<FlowerDto>>> SortFlowers(
        [SwaggerParameter("Параметры сортировки цветов")] [FromBody]
        FlowerSortOptions sortOptions)
    {
        if (sortOptions?.SortOptions == null || !sortOptions.SortOptions.Any())
            return BadRequest("Sort options are required.");

        var sortedFlowers = await flowerStorage.SortFlowers(sortOptions);
        var sortedFlowersDto = _mapper.Map<IEnumerable<FlowerDto>>(sortedFlowers);

        return Ok(sortedFlowersDto);
    }
}

public class FlowerFilterExample : IExamplesProvider<FlowerFilterDto>
{
    public FlowerFilterDto GetExamples()
    {
        return new FlowerFilterDto
        {
            ToxicCategories = new[] { "None" },
            IlluminationInSuites = 400
        };
    }
}

public class FlowerSortOptionsExample : IExamplesProvider<FlowerSortOptions>
{
    public FlowerSortOptions GetExamples()
    {
        return new FlowerSortOptions
        {
            SortOptions = new List<SortOption>
            {
                new() { SortBy = "isToxic", IsDescending = false },
                new() { SortBy = "illuminationInSuites", IsDescending = true }
            }
        };
    }
}