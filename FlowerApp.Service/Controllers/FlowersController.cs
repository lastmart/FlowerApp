using AutoMapper;
using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
using FlowerApp.Service.Services;
using FlowerApp.Service.Storages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using GetFlowerResponse = FlowerApp.DTOs.Response.Flowers.GetFlowerResponse;

namespace FlowerApp.Service.Controllers;

[ApiController]
[Route("api/flowers")]
public class FlowersController : ControllerBase
{
    private readonly IFlowersStorage flowersStorage;
    private readonly IValidator<Pagination> paginationValidator;
    private readonly IMapper mapper;

    public FlowersController(
        IValidator<Pagination> paginationValidator,
        IMapper mapper, IFlowersStorage flowersStorage)
    {
        this.paginationValidator = paginationValidator;
        this.mapper = mapper;
        this.flowersStorage = flowersStorage;
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
    /// <param name="searchString">Поиск по названию цветка</param>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="filterParams">Параметры фильтрации цветов</param>
    /// <param name="sortOption">Параметры сортировки цветов</param>
    /// <returns>Список цветов с основной информацией</returns>
    [HttpGet("{searchString}")]
    public async Task<ActionResult<GetFlowerResponse>> Get(
        string? searchString,
        [FromQuery] Pagination pagination,
        [FromQuery] FlowerFilterParams filterParams,
        [FromQuery] RawSortOption sortOption
    )
    {
        var paginationValidationResult = await paginationValidator.ValidateAsync(pagination);
        if (!paginationValidationResult.IsValid)
            return BadRequest(paginationValidationResult.Errors);

        var getFlowerResponse = await flowersStorage.Get(searchString, pagination, filterParams, mapper.Map<FlowerSortOptions>(sortOption));
        return Ok(getFlowerResponse);
    }

    /// <summary>
    ///     Получение иформации о конкретном цветке
    /// </summary>
    /// <param name="id">Идентификатор цветка</param>
    /// <returns>Детальная информация о цветке</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var flowers = await flowersStorage.Get(id);
        return Ok(flowers);
    }
}