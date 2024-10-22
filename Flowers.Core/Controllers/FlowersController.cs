using FlowersCareAPI.Models;
using FlowersCareAPI.Storages.FlowersStorage;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FlowersCareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlowersController : ControllerBase
{
    // private readonly IFlowersStorage flowersStorage;
    //
    // public FlowersController(IFlowersStorage flowersStorage)
    // {
    //     this.flowersStorage = flowersStorage;
    // }
    private static readonly List<Flower> flowers = new List<Flower>
    {
    new Flower(1, "Rosa", "Rose", "A beautiful flower", 7, 3, 
        "Care instructions", "rose.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 5,
            DurationInHours = 12,
            IsShadeTolerant = true
        },
        [ToxicCategory.Baby]
    ),
    new Flower(2, "Tulipa", "Tulip", "A bright flower", 5, 2, 
        "Care instructions", "tulip.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 4,
            DurationInHours = 10,
            IsShadeTolerant = false
        }
    ),
    new Flower(3, "Lavandula", "Lavender", "A fragrant flower", 14, 6, 
        "Prefers dry soil", "lavender.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 6,
            DurationInHours = 8,
            IsShadeTolerant = true
        },
        [ToxicCategory.Cat]
    ),
    new Flower(4, "Chlorophytum comosum", "Spider Plant", "Easy to care for", 10, 12, 
        "Low maintenance plant", "spiderplant.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 3,
            DurationInHours = 6,
            IsShadeTolerant = true
        }
    ),
    new Flower(5, "Aloe vera", "Aloe", "Medicinal plant", 21, 18, 
        "Requires little water", "aloe.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 8,
            DurationInHours = 14,
            IsShadeTolerant = false
        },
        [ToxicCategory.Dog]
    ),
    new Flower(6, "Orchidaceae", "Orchid", "Exotic flower", 3, 24, 
        "Requires indirect light", "orchid.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 7,
            DurationInHours = 10,
            IsShadeTolerant = false
        }
    ),
    new Flower(7, "Dracaena", "Dracaena", "Popular houseplant", 3, 15, 
        "Prefers bright light", "dracaena.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 5,
            DurationInHours = 12,
            IsShadeTolerant = true
        },
        [ToxicCategory.Baby, ToxicCategory.People]
    ),
    new Flower(8, "Anthurium", "Anthurium andraeanum", "Houseplant", 25, 20, 
        "Requires moderate watering", "anthurium.jpg", 
        new LightParameters
        {
            IlluminationInSuites = 6,
            DurationInHours = 12,
            IsShadeTolerant = true
        },
        [ToxicCategory.Cat, ToxicCategory.Dog]
    )};

    [HttpGet]
    [SwaggerOperation(Summary = "Получить все цветы", Description = "Возвращает список всех доступных цветов.")]
    public ActionResult<IEnumerable<Flower>> GetFlowers()
    {
        return Ok(flowers);
    }

    [HttpGet("byId/{fid}")]
    [SwaggerOperation(Summary = "Получить цветок по ID", Description = "Возвращает цветок с указанным ID.")]
    public ActionResult<Flower> GetFlowerByScientificName(int fid)
    {
        var flower = flowers.FirstOrDefault(f => f.FId == fid);
        return flower is null
            ? NotFound()
            : Ok(flower);
    }

    [HttpGet("byName/{name}")]
    [SwaggerOperation(Summary = "Получить цветок по имени", Description = "Возвращает цветок по его имени или научному имени.")]
    public ActionResult<Flower> GetFlowerByName(string name)
    {
        var flower = flowers.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                                                 f.ScientificName.Equals(name, StringComparison.OrdinalIgnoreCase));

        return flower is null ? NotFound() : Ok(flower);
    }

    [HttpPost("filter")]
    [SwaggerOperation(Summary = "Фильтровать цветы", 
        Description = "Возвращает отфильтрованный список цветов по указанным критериям. Доступные поля для фильтрации:\n" +
                      "- wateringFrequency: частота полива\n" +
                      "- transplantFrequency: частота пересадки\n" +
                      "- illuminationInSuites: освещённость в люксах\n" +
                      "- durationInHours: продолжительность освещения в часах\n" +
                      "- isShadeTolerant: теневыносливость\n" +
                      "- toxicCategories: категории токсичности (Baby, Cat, Dog, People)\n" +
                      "Чтобы получить не токсичные цветы, нужно указать пустой массив для токсичных категорий.")]
    public ActionResult<IEnumerable<Flower>> FilterFlowers([FromBody] FlowerFilter filter)
    {
        var filteredFlowers = flowers.AsQueryable();
    
        if (filter.WateringFrequency.HasValue)
            filteredFlowers = filteredFlowers.Where(f => f.WateringFrequency == filter.WateringFrequency.Value);
    
        if (filter.TransplantFrequency.HasValue)
            filteredFlowers = filteredFlowers.Where(f => f.TransplantFrequency == filter.TransplantFrequency.Value);
    
        if (filter.IlluminationInSuites.HasValue)
            filteredFlowers = filteredFlowers.Where(f => f.LightParameters.IlluminationInSuites == filter.IlluminationInSuites.Value);

        if (filter.DurationInHours.HasValue)
            filteredFlowers = filteredFlowers.Where(f => f.LightParameters.DurationInHours == filter.DurationInHours.Value);

        if (filter.IsShadeTolerant.HasValue)
            filteredFlowers = filteredFlowers.Where(f => f.LightParameters.IsShadeTolerant == filter.IsShadeTolerant.Value);
        
        if (filter.ToxicCategories != null && filter.ToxicCategories.Length != 0)
        {
            filteredFlowers = filteredFlowers.Where(f => 
                f.ToxicCategories != null && 
                f.ToxicCategories.Length == filter.ToxicCategories.Length && 
                !f.ToxicCategories.Except(filter.ToxicCategories).Any()); 
        } else 
        {
            filteredFlowers = filteredFlowers.Where(
                f => f.ToxicCategories == null || f.ToxicCategories.Length == 0);
        }
    
        return Ok(filteredFlowers.ToList());
    }
    
    [HttpPost("sort")]
    [SwaggerOperation(
        Summary = "Сортировать цветы", 
        Description = "Возвращает отсортированный список цветов по указанным критериям. Доступные поля для сортировки:\n" +
                      "- wateringFrequency: частота полива\n" +
                      "- name: название цветка\n" +
                      "- scientificname: научное название\n" +
                      "- illuminationInSuites: освещённость в люксах\n" +
                      "- durationInHours: продолжительность освещения в часах\n" +
                      "- isShadeTolerant: теневыносливость\n" +
                      "- transplantFrequency: частота пересадки\n" +
                      "- isToxic: токсичность"
    )]
    public IActionResult SortFlowers([FromBody] FlowerSortOptions sortOptions)
    {
        IOrderedEnumerable<Flower> orderedFlowers = sortOptions
            .SortOptions
            .Aggregate<SortOption?, 
                IOrderedEnumerable<Flower>>(null, (current, sortOption) 
                => current == null 
                    ? ApplySorting(flowers, sortOption) 
                    : ApplyThenSorting(current, sortOption));

        return Ok(orderedFlowers);
    }

    private IOrderedEnumerable<Flower> ApplySorting(IEnumerable<Flower> flowers, SortOption sortOption)
    {
        switch (sortOption.SortBy.ToLower())
        {
            case "name":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.Name) 
                    : flowers.OrderBy(f => f.Name);
            case "wateringfrequency":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.WateringFrequency) 
                    : flowers.OrderBy(f => f.WateringFrequency);
            case "scientificname":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.ScientificName) 
                    : flowers.OrderBy(f => f.ScientificName);
            case "illuminationinsuites":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.LightParameters.IlluminationInSuites) 
                    : flowers.OrderBy(f => f.LightParameters.IlluminationInSuites);
            case "durationinhours":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.LightParameters.DurationInHours) 
                    : flowers.OrderBy(f => f.LightParameters.DurationInHours);
            case "isshadetolerant":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.LightParameters.IsShadeTolerant) 
                    : flowers.OrderBy(f => f.LightParameters.IsShadeTolerant);
            case "transplantfrequency":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.TransplantFrequency) 
                    : flowers.OrderBy(f => f.TransplantFrequency);
            case "istoxic":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.IsToxic) 
                    : flowers.OrderBy(f => f.IsToxic);
            default:
                throw new ArgumentException("Invalid sort option.");
        }
    }

    private IOrderedEnumerable<Flower> ApplyThenSorting(IOrderedEnumerable<Flower> orderedFlowers, SortOption sortOption)
    {
        switch (sortOption.SortBy.ToLower())
        {
            case "name":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending<Flower, string>(f => f.Name) 
                    : orderedFlowers.ThenBy<Flower, string>(f => f.Name);
            case "wateringfrequency":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending<Flower, int>(f => f.WateringFrequency) 
                    : orderedFlowers.ThenBy<Flower, int>(f => f.WateringFrequency);
            case "scientificname":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending<Flower, string>(f => f.ScientificName) 
                    : orderedFlowers.ThenBy<Flower, string>(f => f.ScientificName);
            case "illuminationinsuites":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending(f => f.LightParameters.IlluminationInSuites) 
                    : orderedFlowers.ThenBy(f => f.LightParameters.IlluminationInSuites);
            case "durationinhours":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending(f => f.LightParameters.DurationInHours) 
                    : orderedFlowers.ThenBy(f => f.LightParameters.DurationInHours);
            case "isshadetolerant":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending(f => f.LightParameters.IsShadeTolerant) 
                    : orderedFlowers.ThenBy(f => f.LightParameters.IsShadeTolerant);
            case "transplantfrequency":
                return sortOption.IsDescending 
                    ? orderedFlowers.ThenByDescending<Flower, int>(f => f.TransplantFrequency) 
                    : orderedFlowers.ThenBy<Flower, int>(f => f.TransplantFrequency);
            case "istoxic":
                return sortOption.IsDescending 
                    ? flowers.OrderByDescending(f => f.IsToxic) 
                    : flowers.OrderBy(f => f.IsToxic);
            default:
                throw new ArgumentException("Invalid sort option.");
        }
    }
}