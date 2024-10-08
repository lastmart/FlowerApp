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
}