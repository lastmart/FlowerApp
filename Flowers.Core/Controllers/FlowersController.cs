using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class FlowersController : ControllerBase
{
    private static List<Flower> flowers = new List<Flower>
    {
        new Flower(1, "Rose", false, 3, LightLevel.High, "Once a year"),
        new Flower(2, "Cactus", false, 7, LightLevel.Medium, "Once every 3 years")
    };
    
    [HttpGet]
    public ActionResult<IEnumerable<Flower>> GetFlowers()
    {
        return Ok(flowers);
    }

    [HttpGet("{id}")]
    public ActionResult<Flower> GetFlowerById(int id)
    {
        var flower = flowers.FirstOrDefault(f => f.Id == id);
        if (flower == null)
        {
            return NotFound();
        }
        return Ok(flower);
    }
}

