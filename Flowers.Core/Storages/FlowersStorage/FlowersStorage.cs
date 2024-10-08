using FlowersCareAPI.Models;

namespace FlowersCareAPI.Storages.FlowersStorage;

public class FlowersStorage : IFlowersStorage
{
    private static readonly List<Flower> flowers =
    [
        new Flower(
            "Rosa",
            "Rose",
            "A beautiful and fragrant flower, often associated with love and romance.",
            false,
            3,
            LightLevel.High,
            12,
            "Requires regular watering and plenty of sunlight.",
            "https://example.com/roses.jpg"
        ),

        new Flower(
            "Cactaceae",
            "Cactus",
            "A succulent plant known for its ability to thrive in dry conditions.",
            false,
            7,
            LightLevel.Medium,
            36,
            "Needs minimal watering and bright, indirect light.",
            "https://example.com/cactus.jpg"
        )
    ];

    public IEnumerable<Flower> GetAll()
    {
        return flowers;
    }

    public Flower? GetByScientificName(string scientificName)
    {
        return flowers.Find(flower => flower.ScientificName == scientificName) ?? null;
    }
}