using FlowerApp.Data;
using FlowerApp.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FlowerApp.Service;

public class DataSeeder
{
    private readonly FlowerAppContext _context;

    public DataSeeder(FlowerAppContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        
        if (!_context.Flowers.Any() && !_context.LightParameters.Any())
        {
            var lightParameters1 = new LightParameters { Id = 1, DurationInHours = 8, IlluminationInSuites = 500 };
            var lightParameters2 = new LightParameters { Id = 2, DurationInHours = 10, IlluminationInSuites = 600 };
            var lightParameters3 = new LightParameters { Id = 3, DurationInHours = 6, IlluminationInSuites = 400 };

            _context.LightParameters.AddRange(lightParameters1, lightParameters2, lightParameters3);

            var flowers = new List<Flower>
            {
                new Flower
                {
                    Id = 1,
                    Name = "Rose",
                    ScientificName = "Rosa",
                    AppearanceDescription = "Beautiful red flower",
                    CareDescription = "Needs regular watering and sunlight",
                    PhotoUrl = "https://example.com/rose.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters1.Id,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Baby
                },
                new Flower
                {
                    Id = 2,
                    Name = "Lily",
                    ScientificName = "Lilium",
                    AppearanceDescription = "Elegant white flower",
                    CareDescription = "Needs moderate watering and indirect sunlight",
                    PhotoUrl = "https://example.com/lily.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters2.Id,
                    ToxicCategory = ToxicCategory.Cat | ToxicCategory.Dog
                },
                new Flower
                {
                    Id = 3,
                    Name = "Tulip",
                    ScientificName = "Tulipa",
                    AppearanceDescription = "Bright and colorful flower",
                    CareDescription = "Needs well-drained soil and moderate sunlight",
                    PhotoUrl = "https://example.com/tulip.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters3.Id,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Baby | ToxicCategory.Cat | ToxicCategory.Dog
                },
                new Flower
                {
                    Id = 4,
                    Name = "Sunflower",
                    ScientificName = "Helianthus",
                    AppearanceDescription = "Large yellow flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://example.com/sunflower.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters1.Id,
                    ToxicCategory = ToxicCategory.None
                },
                new Flower
                {
                    Id = 5,
                    Name = "Orchid",
                    ScientificName = "Orchidaceae",
                    AppearanceDescription = "Exotic and colorful flower",
                    CareDescription = "Needs indirect sunlight and careful watering",
                    PhotoUrl = "https://example.com/orchid.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters2.Id,
                    ToxicCategory = ToxicCategory.People | ToxicCategory.Baby  | ToxicCategory.Dog
                },
                new Flower
                {
                    Id = 6,
                    Name = "Daisy",
                    ScientificName = "Bellis perennis",
                    AppearanceDescription = "Simple and cheerful flower",
                    CareDescription = "Needs moderate sunlight and regular watering",
                    PhotoUrl = "https://example.com/daisy.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters3.Id,
                    ToxicCategory = ToxicCategory.None
                },
                new Flower
                {
                    Id = 7,
                    Name = "Carnation",
                    ScientificName = "Dianthus caryophyllus",
                    AppearanceDescription = "Fragrant and colorful flower",
                    CareDescription = "Needs well-drained soil and moderate sunlight",
                    PhotoUrl = "https://example.com/carnation.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters1.Id,
                    ToxicCategory = ToxicCategory.None
                },
                new Flower
                {
                    Id = 8,
                    Name = "Peony",
                    ScientificName = "Paeonia",
                    AppearanceDescription = "Large and fragrant flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://example.com/peony.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters2.Id,
                    ToxicCategory = ToxicCategory.None
                },
                new Flower
                {
                    Id = 9,
                    Name = "Marigold",
                    ScientificName = "Tagetes",
                    AppearanceDescription = "Bright and cheerful flower",
                    CareDescription = "Needs full sunlight and regular watering",
                    PhotoUrl = "https://example.com/marigold.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters3.Id,
                    ToxicCategory = ToxicCategory.None
                },
                new Flower
                {
                    Id = 10,
                    Name = "Lavender",
                    ScientificName = "Lavandula",
                    AppearanceDescription = "Fragrant and purple flower",
                    CareDescription = "Needs full sunlight and well-drained soil",
                    PhotoUrl = "https://example.com/lavender.jpg",
                    WateringFrequency = DateTime.UtcNow,
                    TransplantFrequency = DateTime.UtcNow,
                    LightParametersId = lightParameters1.Id,
                    ToxicCategory = ToxicCategory.None
                }
            };

            _context.Flowers.AddRange(flowers);

            await _context.SaveChangesAsync();
        }
    }
}