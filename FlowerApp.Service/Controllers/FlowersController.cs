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
    ///   * (-)wateringFrequency - частота полива
    ///   * (-)name - название
    ///   * (-)scientificName - научное название
    ///   * (-)illuminationInSuites - освещенность
    ///   * (-)isToxic - токсичность
    /// - Знак перед полем сортировки - направление сортировки:
    ///   * если знак не стоит, то сортируем по возрастанию
    ///   * если стоит "-", то сортируем по убыванию
    /// </remarks>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="filterParams">Параметры фильтрации цветов</param>
    /// <param name="sortOption">Параметры сортировки цветов</param>
    /// <param name="searchQuery">Параметр поиска по строке</param>
    /// <returns>Список цветов с основной информацией</returns>
    [HttpGet]
    public async Task<ActionResult<GetFlowerResponse>> Get(
        [FromQuery] Pagination pagination,
        [FromQuery] FlowerFilterParams filterParams,
        [FromQuery] FlowerSortOptions sortOption,
        [FromQuery] string? searchQuery
    )
    {
        Console.WriteLine($"sortOption {sortOption.SortOptions.Count}");
        var paginationValidationResult = await paginationValidator.ValidateAsync(pagination);
        if (!paginationValidationResult.IsValid)
            return BadRequest(paginationValidationResult.Errors);

        var getFlowerResponse = await flowersService.Get(pagination, filterParams, sortOption, searchQuery);
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
}