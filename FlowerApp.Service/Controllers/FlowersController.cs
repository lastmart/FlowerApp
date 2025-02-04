using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
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

    public FlowersController(
        IValidator<Pagination> paginationValidator,
        IFlowersStorage flowersStorage
    )
    {
        this.paginationValidator = paginationValidator;
        this.flowersStorage = flowersStorage;
    }

    /// <summary>
    ///     Получение списка цветов с фильтрацией и сортировкой
    /// </summary>
    /// <param name="searchString">Поиск по названию цветка</param>
    /// <param name="pagination">Параметры пагинации (Skip, Take)</param>
    /// <param name="filterParams">Параметры фильтрации цветов</param>
    /// <param name="sortParams">Параметры сортировки цветов</param>
    /// <returns>Список цветов с основной информацией</returns>
    [HttpGet]
    public async Task<ActionResult<GetFlowerResponse>> Get(
        string? searchString,
        [FromQuery] Pagination pagination,
        [FromQuery] FlowerFilterParams filterParams,
        [FromQuery] FlowerSortParams sortParams
    )
    {
        var paginationValidationResult = await paginationValidator.ValidateAsync(pagination);
        if (!paginationValidationResult.IsValid)
            return BadRequest(paginationValidationResult.Errors);

        var getFlowerResponse = await flowersStorage.Get(searchString, pagination, filterParams, sortParams);
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