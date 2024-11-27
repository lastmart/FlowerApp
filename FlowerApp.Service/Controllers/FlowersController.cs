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

    /// <summary>
    /// Получение списка цветов с фильтрацией, сортировкой и пагинацией
    /// </summary>
    /// <remarks>
    /// Параметры пагинации:
    /// - Skip: количество пропускаемых элементов
    /// - Take: количество возвращаемых элементов (максимум 50)
    ///
    /// Параметры фильтрации:
    /// - WateringFrequency: массив частоты полива
    ///   * OnceAWeek - раз в неделю
    ///   * TwiceAWeek - дважды в неделю
    ///   * OnceEveryTwoWeeks - раз в две недели
    ///   * OnceAMonth - раз в месяц
    ///
    /// - ToxicCategories: массив категорий токсичности (можно комбинировать)
    ///   * None - не токсично
    ///   * Kids - токсично для детей
    ///   * Pets - токсично для домашних животных
    ///   * People - токсично для взрослых людей
    ///
    /// - Illumination: массив типов освещения
    ///   * Bright - яркое освещение
    ///   * PartialShade - частичная тень
    ///   * AverageIllumination - среднее освещение
    ///
    /// Параметры сортировки:
    /// - SortBy: поле для сортировки
    ///   * wateringFrequency - частота полива
    ///   * name - название
    ///   * scientificName - научное название
    ///   * illuminationInSuites - освещенность
    ///   * isToxic - токсичность
    /// - IsDescending: направление сортировки (true - по убыванию, false - по возрастанию)
    /// </remarks>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="filterParams">Параметры фильтрации цветов</param>
    /// <param name="sortOption">Параметры сортировки цветов</param>
    /// <returns>Список цветов с основной информацией</returns>
    [HttpPost]
    public async Task<ActionResult<GetFlowerResponse>> Post(
        [FromQuery] Pagination pagination,
        [FromQuery] FlowerFilterParams filterParams,
        [FromBody] FlowerSortOptions sortOption
    )
    {
        var paginationValidationResult = await paginationValidator.ValidateAsync(pagination);
        if (!paginationValidationResult.IsValid)
            return BadRequest(paginationValidationResult.Errors);

        var getFlowerResponse = await flowersService.Get(pagination, filterParams, sortOption);
        return Ok(getFlowerResponse);
    }
    
    /// <summary>
    /// Получить цветок по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор цветка</param>
    /// <returns>Детальная информация о цветке</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var flower = await flowersService.Get(id);
        return flower != null ? Ok(flower) : NotFound();
    }

    /// <summary>
    /// Получить цветок по названию или научному названию 
    /// </summary>
    /// <param name="name">Название цветка</param>
    /// <returns>Детальная информация о цветке</returns>
    [HttpGet("{name}")]
    public async Task<IActionResult> Get(string name)
    {
        var flower = await flowersService.Get(name);
        return flower != null ? Ok(flower) : NotFound();
    }
}