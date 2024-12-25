using AutoMapper;
using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
using FlowerApp.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using GetFlowerResponse = FlowerApp.DTOs.Response.Flowers.GetFlowerResponse;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/flowers")]
public class FlowersController : ControllerBase
{
    private readonly IFlowersService flowersService;
    private readonly IValidator<Pagination> paginationValidator;
    private readonly IMapper mapper;

    public FlowersController(
        IFlowersService flowersService,
        IValidator<Pagination> paginationValidator,
        IMapper mapper
    )
    {
        this.flowersService = flowersService;
        this.paginationValidator = paginationValidator;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получение списка цветов с фильтрацией, сортировкой и пагинацией
    /// </summary>
    /// <remarks>
    ///     Параметры пагинации:
    ///     - Skip: количество пропускаемых элементов
    ///     - Take: количество возвращаемых элементов (максимум 50)
    ///     Параметры фильтрации:
    ///     - WateringFrequency: массив частоты полива
    ///     * OnceAWeek - раз в неделю
    ///     * TwiceAWeek - дважды в неделю
    ///     * OnceEveryTwoWeeks - раз в две недели
    ///     * OnceAMonth - раз в месяц
    ///     - ToxicCategories: массив категорий токсичности (можно комбинировать)
    ///     * None - не токсично
    ///     * Kids - токсично для детей
    ///     * Pets - токсично для домашних животных
    ///     * People - токсично для взрослых людей
    ///     - Illumination: массив типов освещения
    ///     * Bright - яркое освещение
    ///     * PartialShade - частичная тень
    ///     * AverageIllumination - среднее освещение
    ///     Параметры сортировки:
    ///     - SortBy: поле для сортировки
    ///     * wateringFrequency - частота полива
    ///     * name - название
    ///     * scientificName - научное название
    ///     * illuminationInSuites - освещенность
    ///     * isToxic - токсичность
    ///     - IsDescending: направление сортировки (true - по убыванию, false - по возрастанию)
    /// </remarks>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="filterParams">Параметры фильтрации цветов</param>
    /// <param name="sortOption">Параметры сортировки цветов</param>
    /// <returns>Список цветов с основной информацией</returns>
    [HttpGet]
    public async Task<ActionResult<GetFlowerResponse>> Get(
        [FromQuery] Pagination pagination,
        [FromQuery] FlowerFilterParams filterParams,
        [FromQuery] RawSortOption sortOption
    )
    {
        var paginationValidationResult = await paginationValidator.ValidateAsync(pagination);
        if (!paginationValidationResult.IsValid)
            return BadRequest(paginationValidationResult.Errors);

        var getFlowerResponse = await flowersService.Get(pagination, filterParams, mapper.Map<FlowerSortOptions>(sortOption));
        return Ok(getFlowerResponse);
    }

    /// <summary>
    ///     Получение иформации о конкретном цветке
    /// </summary>
    /// <param name="searchString">Идентификатор или префиксу названия цветка</param>
    /// <returns>Детальная информация о цветке</returns>
    [HttpGet("{searchString}")]
    public async Task<IActionResult> Get(string searchString)
    {
        var flowers = await flowersService.Get(searchString);
        return Ok(flowers);
    }
}