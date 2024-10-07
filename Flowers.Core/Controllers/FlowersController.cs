using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class FlowersController : ControllerBase
{
    private static List<Flower> flowers = new List<Flower>
    {
        new Flower(
            scientificName: "Rosa",
            name: "Rose",
            descriptionFlower: "A beautiful and fragrant flower, often associated with love and romance.",
            isToxic: false,
            wateringFrequency: 3,
            lightRequirements: LightLevel.High,
            transplantFrequency: 12,
            descriptionCare: "Requires regular watering and plenty of sunlight.",
            photoUrl: "https://example.com/roses.jpg"
        ),
        new Flower(
            scientificName: "Cactaceae",
            name: "Cactus",
            descriptionFlower: "A succulent plant known for its ability to thrive in dry conditions.",
            isToxic: false,
            wateringFrequency: 7,
            lightRequirements: LightLevel.Medium,
            transplantFrequency: 36,
            descriptionCare: "Needs minimal watering and bright, indirect light.",
            photoUrl: "https://example.com/cactus.jpg"
        )
    };
    
    [HttpGet]
    public ActionResult<IEnumerable<Flower>> GetFlowers()
    {
        return Ok(flowers);
    }

    [HttpGet("byScientificName/{scientificName}")]
    public ActionResult<Flower> GetFlowerByScientificName(string scientificName)
    {
        var flower = flowers.FirstOrDefault(f => f.ScientificName.Equals(scientificName, StringComparison.OrdinalIgnoreCase));
        if (flower == null)
        {
            return NotFound();
        }
        return Ok(flower);
    }
}