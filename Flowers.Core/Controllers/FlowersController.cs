using FlowersCareAPI.Models;
using FlowersCareAPI.Storages.FlowersStorage;
using Microsoft.AspNetCore.Mvc;

namespace FlowersCareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlowersController : ControllerBase
{
    private readonly IFlowersStorage flowersStorage;

    public FlowersController(IFlowersStorage flowersStorage)
    {
        this.flowersStorage = flowersStorage;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Flower>> GetFlowers()
    {
        return Ok(flowersStorage.GetAll());
    }

    [HttpGet("byScientificName/{scientificName}")]
    public ActionResult<Flower> GetFlowerByScientificName(string scientificName)
    {
        var flower = flowersStorage.GetByScientificName(scientificName);
        return flower is null
            ? NotFound()
            : Ok(flower);
    }
    
    [HttpGet("byName/{name}")]
    public ActionResult<Flower> GetFlowerByName(string name)
    {
        var flower = flowersStorage.GetAll()
            .FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                                 f.ScientificName.Equals(name, StringComparison.OrdinalIgnoreCase));
    
        return flower is null ? NotFound() : Ok(flower);
    }
    
    [HttpPost("filter")]
    public ActionResult<IEnumerable<Flower>> FilterFlowers([FromBody] FlowerFilter filter)
    {
        var flowers = flowersStorage.GetAll();

        if (filter.WateringFrequency.HasValue)
            flowers = flowers.Where(f => f.WateringFrequency == filter.WateringFrequency.Value);

        if (filter.LightRequirements.HasValue)
            flowers = flowers.Where(f => f.LightRequirements == filter.LightRequirements.Value);

        if (filter.IsToxic.HasValue)
            flowers = flowers.Where(f => f.IsToxic == filter.IsToxic.Value);

        if (filter.TransplantFrequency.HasValue)
            flowers = flowers.Where(f => f.TransplantFrequency == filter.TransplantFrequency.Value);

        return Ok(flowers.ToList());
    }
    
    [HttpPost("sort")]
    public ActionResult<IEnumerable<Flower>> SortFlowers([FromBody] FlowerSortOptions sortOptions)
    {
        var flowers = flowersStorage.GetAll();
        flowers = sortOptions.SortBy.ToLower() switch
        {
            "wateringfrequency" => sortOptions.IsDescending
                ? flowers.OrderByDescending(f => f.WateringFrequency)
                : flowers.OrderBy(f => f.WateringFrequency),

            "name" => sortOptions.IsDescending
                ? flowers.OrderByDescending(f => f.Name)
                : flowers.OrderBy(f => f.Name),

            "scientificname" => sortOptions.IsDescending
                ? flowers.OrderByDescending(f => f.ScientificName)
                : flowers.OrderBy(f => f.ScientificName),

            "lightrequirements" => sortOptions.IsDescending
                ? flowers.OrderByDescending(f => f.LightRequirements)
                : flowers.OrderBy(f => f.LightRequirements),

            "transplantfrequency" => sortOptions.IsDescending
                ? flowers.OrderByDescending(f => f.TransplantFrequency)
                : flowers.OrderBy(f => f.TransplantFrequency),

            "istoxic" => sortOptions.IsDescending
                ? flowers.OrderByDescending(f => f.IsToxic)
                : flowers.OrderBy(f => f.IsToxic),

            _ => flowers 
        };

        return Ok(flowers.ToList());
    }

}